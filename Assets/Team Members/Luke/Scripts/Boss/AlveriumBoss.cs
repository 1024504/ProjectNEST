using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class AlveriumBoss : EnemyBody, IControllable, ISense
{
	private Transform _transform;
	private Rigidbody2D _rb;
	[HideInInspector] public Animator anim;
	private AntAIAgent _aiAgent;
	public Transform aimTarget;
	public Transform headTransform;
	public Transform tailAimTransform;
	public Transform projectileSpawnTransform;
	public GameObject projectilePrefab;
	public Renderer localProjectileRenderer;
	public float projectileCooldownDuration;
	public float lowHealthProjectileCooldownDuration;
	public float meleeAttackDamage;
	public float meleeCooldownDuration;
	[HideInInspector] public float tailAngle;
	public float minTailAngle;
	public float maxTailAngle;
	private Vector3 _defaultAimTarget;
	public Transform view;
	
	public Transform target;


	public float walkSpeed;
	public float runSpeed;
	[HideInInspector]
	public float currentMoveSpeed;
	private float _lateralInput;
	public bool shootCoolingDown;
	public bool targetInMeleeRange;
	public bool canRun;
	public bool canMelee = true;
	public bool canDealDamage;

	private bool playerBelowHead;
	public bool canSwapMode = true;
	
	// Events
	public Action OnShootBuildup;
	public Action OnShoot;
	public Action OnMelee;
	public Action OnWalk;
	public Action OnRun;
	public Action OnPrepareToRun;

	protected override void OnEnable()
	{
		base.OnEnable();
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		_aiAgent = GetComponent<AntAIAgent>();
		_defaultAimTarget = aimTarget.position;
	}

	private void Start()
	{
		target = GameManager.Instance.playerController.GameplayAgent.transform;
	}
	
	private void FixedUpdate()
	{
		Move();
	}
	
	private void Update()
	{
		Aim();
	}

	public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
	{
		aWorldState.Set(BossScenario.CanMelee, canMelee);
		aWorldState.Set(BossScenario.CanRun, canRun);
		aWorldState.Set(BossScenario.CanShoot, !shootCoolingDown);
		aWorldState.Set(BossScenario.TargetInMeleeRange, targetInMeleeRange);
		aWorldState.Set(BossScenario.TargetIsBelowHead, CheckTargetBelowHead());
		aWorldState.Set(BossScenario.TargetIsDead, false);
	}

	private void Move()
	{
		_rb.velocity = new Vector2(_lateralInput * currentMoveSpeed, _rb.velocity.y);
	}

	private void Aim()
	{
		if (target == null || !_aiAgent.isActiveAndEnabled) aimTarget.position = _defaultAimTarget;
		else aimTarget.position = target.position;

		tailAngle = Vector3.SignedAngle(Vector3.up, aimTarget.position - tailAimTransform.position, tailAimTransform.TransformDirection(Vector3.right));
		tailAngle = Mathf.Clamp(tailAngle, minTailAngle, maxTailAngle);
		
		tailAimTransform.rotation = Quaternion.Euler(tailAngle-90, 90+view.eulerAngles.y, 0);

		if (CheckTargetBelowHead()) return;
		if (!(tailAngle <= minTailAngle)) return;
		if (Mathf.Abs((target.position - _transform.position).x) <= 2f) return;
		if (view.eulerAngles.y == 0) view.rotation = Quaternion.Euler(0, 180, 0);
		else view.rotation = Quaternion.Euler(0, 0, 0);
	}

	public IEnumerator PrepareToRun()
	{
		OnPrepareToRun?.Invoke();
		string currentStateName = anim.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
		yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(currentStateName));
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		canRun = true;
	}
	
	private IEnumerator ShootAnimation()
	{
		OnShootBuildup?.Invoke();
		string currentStateName = anim.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
		yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(currentStateName));
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		OnShoot?.Invoke();
		currentStateName = anim.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
		yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(currentStateName));
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		Shoot();
	}

	private void Shoot()
	{
		GameObject go = Instantiate(projectilePrefab, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
		go.GetComponent<BossProjectile>().SetTarget(target);
		localProjectileRenderer.enabled = false;
		StartCoroutine(ShootCooldown());
	}
	
	private IEnumerator ShootCooldown()
	{
		if (health.HealthLevel > health.maxHealth*0.5f) yield return new WaitForSeconds(projectileCooldownDuration);
		else yield return new WaitForSeconds(lowHealthProjectileCooldownDuration);
		shootCoolingDown = false;
		localProjectileRenderer.enabled = true;
	}
	
	private IEnumerator MeleeAttackAnimation()
	{
		OnMelee?.Invoke();
		string currentStateName = anim.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
		yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(currentStateName));
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		CooldownAttack();
	}
	
	private void CooldownAttack()
	{
		StartCoroutine(CooldownAttackTimer());
	}
	
	private IEnumerator CooldownAttackTimer()
	{
		canMelee = false;
		yield return new WaitForSeconds(meleeCooldownDuration);
		canMelee = true;
	}

	private bool CheckTargetBelowHead()
	{
		if (target == null) return false;
		if (!canSwapMode) return playerBelowHead;
		playerBelowHead = target.position.y < _transform.position.y + headTransform.localPosition.y;
		if (!playerBelowHead) canRun = false;
		return playerBelowHead;
	}

	public void MovePerformed(float lateralInput)
    {
	    if (lateralInput != 0) lateralInput = Mathf.Sign(lateralInput);
	    
	    _lateralInput = lateralInput;

	    if (!CheckTargetBelowHead()) return;
	    
	    if (_lateralInput < 0) view.localRotation = Quaternion.identity;
	    else if (_lateralInput > 0) view.localRotation = Quaternion.Euler(0, 180, 0);
    }

    public void MoveCancelled()
    {
	    _lateralInput = 0;
    }

    public void AimPerformedMouse(Vector2 input)
    {
	    
    }

    public void AimPerformedGamepad(Vector2 input)
    {
	    
    }

    public void AimCancelled()
    {
	    
    }

    public void JumpPerformed()
    {
	    
    }

    public void JumpCancelled()
    {
	    
    }

    public void ShootPerformed()
    {
	    if (shootCoolingDown) return;
	    shootCoolingDown = true;

	    StartCoroutine(ShootAnimation());
    }

    public void ShootCancelled()
    {
	    
    }

    public void Action1Performed()
    {
	    StartCoroutine(MeleeAttackAnimation());
    }

    public void Action1Cancelled()
    {
	    
    }

    public void Action2Performed()
    {
	    
    }

    public void Action2Cancelled()
    {
	    
    }

    public void Action3Performed()
    {
	    
    }

    public void Action3Cancelled()
    {
	    
    }

    public void PausePerformed()
    {
	    
    }

    public void PauseCancelled()
    {
	    
    }

    public void ResumePerformed()
    {
	    
    }

    public void ResumeCancelled()
    {
	    
    }

    public void Weapon1Performed()
    {
	    
    }

    public void Weapon1Cancelled()
    {
	    
    }

    public void Weapon2Performed()
    {
	    
    }

    public void Weapon2Cancelled()
    {
	    
    }

    public void Weapon3Performed()
    {
	    
    }

    public void Weapon3Cancelled()
    {
	    
    }

    public void WeaponScrollPerformed()
    {
	    
    }

    public void WeaponScrollCancelled()
    {
	    
    }

    public void MedKitPerformed()
    {
	    
    }

    public void MedKitCancelled()
    {
	    
    }

    public void SprintPerformed()
    {
	    
    }

    public void SprintCancelled()
    {
	    
    }
    
    public enum BossScenario
    {
	    TargetIsDead = 0,
	    TargetIsBelowHead = 1,
	    TargetInMeleeRange = 2,
	    CanRun = 3,
	    CanShoot = 4,
	    CanMelee = 5
    }
}
