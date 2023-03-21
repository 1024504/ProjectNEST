using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class AlveriumMoth : EnemyBody, IControllable, ISense
{
	public float moveSpeed = 15f;
	public float shootCooldownDuration = 0.5f;
	public float idleDuration = 1f;
	public float patrolDuration = 3f;
	public bool beginsPatrolLeft = true;

	private float _lateralMoveInput;
	private float _verticalMoveInput;
	private bool _shootCoolingDown;
	private bool _canPatrol;
	private bool _hasTarget = false;
	private bool _canHitTarget = false;

	[SerializeField] private Transform view;
	[SerializeField] private Transform tailTransform;
	public Transform aimTransform;
	[SerializeField] private Transform projectileTransform;
	
	public Vector3 localDefaultAimPosition;

	public GameObject projectilePrefab;
	public SpriteRenderer localProjectileRenderer;
	
	private Transform _transform;
	private Rigidbody2D _rb;
	public Action OnIdle;
	public Action OnMoveBurst;
	public Action OnMoveConstant;
	public Action OnAttackBuildup;
	public Action OnAttackShoot;

	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		Move(_lateralMoveInput, _verticalMoveInput);
	}

	private void Update()
	{
		Aim();
	}
	
	public IEnumerator CanPatrolTimer(float duration)
	{
		yield return new WaitForSeconds(duration);
		_canPatrol = !_canPatrol;
	}
	
	public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
	{
		aWorldState.Set(MothScenario.CanPatrol, _canPatrol);
		aWorldState.Set(MothScenario.CanShoot, !_shootCoolingDown);
		aWorldState.Set(MothScenario.HasTarget, _hasTarget);
		aWorldState.Set(MothScenario.CanHitTarget, _canHitTarget);
		aWorldState.Set(MothScenario.AllTargetsDead, false);
	}

	private void Move(float lateralInput, float verticalInput = 0)
	{
		Vector2 input = new Vector2(lateralInput, verticalInput);
		input = input.normalized;

		_rb.velocity = input * moveSpeed;
	}

	private void Aim()
	{
		Vector3 aimPosition = aimTransform.position;
		Vector3 aimLocalPosition = aimTransform.localPosition;

		aimPosition = new Vector3(aimPosition.x, Mathf.Min(aimPosition.y, tailTransform.position.y),
			aimPosition.z);
		
		if (aimLocalPosition.x < 0)
		{
			view.localRotation = Quaternion.identity;
		}
		else if (aimLocalPosition.x > 0)
		{
			view.localRotation = Quaternion.Euler(0, 180, 0);
		}
		tailTransform.LookAt(aimPosition, Vector3.down);
	}

	public void MovePerformed(float lateralInput) => _lateralMoveInput = lateralInput;

	public void MoveCancelled() => _lateralMoveInput = 0;

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
	    _verticalMoveInput += 1;
    }

    public void JumpCancelled()
    {
	    _verticalMoveInput -= 1;
    }

    public void ShootPerformed()
    {
	    if (_shootCoolingDown) return;
	    _shootCoolingDown = true;
	    Instantiate(projectilePrefab, projectileTransform.position, projectileTransform.rotation);
	    localProjectileRenderer.enabled = false;
	    StartCoroutine(ShootCooldown());
    }

    private IEnumerator ShootCooldown()
    {
	    yield return new WaitForSeconds(shootCooldownDuration);
	    localProjectileRenderer.enabled = true;
	    _shootCoolingDown = false;
    }

    public void ShootCancelled()
    {
	    
    }

    public void Action1Performed()
    {
	    
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

    public void MedKitPerformed()
    {
	    
    }

    public void MedKitCancelled()
    {
	    
    }

    public void DashPerformed()
    {
	    
    }

    public void DashHeld()
    {
	    _verticalMoveInput -= 1;
    }

    public void DashCancelled()
    {
	    _verticalMoveInput += 1;
    }
    
    private enum MothScenario
    {
	    AllTargetsDead = 0,
	    HasTarget = 1,
	    CanHitTarget = 2,
	    CanShoot = 3,
	    CanPatrol = 4
    }
}
