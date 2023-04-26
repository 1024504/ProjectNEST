using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlveriumSoldier : EnemyBody, IControllable, ISense
{
	private Transform _transform;
	private Rigidbody2D _rb;
	public Animator anim;
	public Action OnIdle;
	public Action OnWalk;
	public Action OnAttack;
	public Action OnLostTarget;
	public Action OnSpotTarget;
	public Action OnRun;
	public Action OnJump;

	public Transform view;

	public List<Transform> targetLocations;
	public Transform currentTarget;
	public float lastKnownTargetDirection;

    [HideInInspector] public SoldierTerrainDetection terrainDetection;

    public bool beginsPatrolLeft = true;
    public float patrolSpeed = 8f;
    public float chaseSpeed = 15f;
    public float attackRange = 2.5f;
    
	public float currentMoveSpeed = 15;
	[SerializeField] private float gravityScale = 1;
	[SerializeField] private float jumpSpeed = 15;
	[SerializeField] private float wallSuctionStrength = 1;

	public float idleDuration = 2f;
	public float patrolDuration = 3f;
	public float memoryDuration = 5f;
	public float attackDamage = 10f;
	public float attackCooldown = 2f;
	public float jumpCooldownDuration = 2f;
	
	public bool canJump = true;
	private bool _canPatrol = true;
	public bool targetWithinRange = false;
	public bool canAttack = true;
	public bool canDealDamage = true;
	private bool _justJumped;

	private float _lateralMoveInput;

	public float rotateSpeed = 1;

	protected override void OnEnable()
	{
		base.OnEnable();
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
        terrainDetection = (SoldierTerrainDetection)GetComponentInChildren<TerrainDetection>();
        anim = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		Move(_lateralMoveInput);
		RotateToWall();
	}
	
	public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
	{
		aWorldState.Set(SoldierScenario.CanPatrol, _canPatrol);
		aWorldState.Set(SoldierScenario.SeesTarget, ChooseTarget());
		aWorldState.Set(SoldierScenario.TargetWithinRange, targetWithinRange);
		aWorldState.Set(SoldierScenario.CanReachTarget, CanReachTarget());
		aWorldState.Set(SoldierScenario.CanAttack, canAttack);
		aWorldState.Set(SoldierScenario.AllTargetsDead, false);
	}

	public IEnumerator CanPatrolTimer(float delay)
	{
		yield return new WaitForSeconds(delay);
		_canPatrol = !_canPatrol;
	}

	public LayerMask visionLayerMask = 520;

	private bool ChooseTarget()
	{
		if (targetLocations.Count == 0)
		{
			currentTarget = null;
			return false;
		}
		
		KeyValuePair<float, int> closestTarget = new KeyValuePair<float, int>(Mathf.Infinity, -1);
		foreach (Transform target in targetLocations)
		{
			float distance = Vector2.Distance(_transform.position, target.position);
			if (distance < closestTarget.Key) closestTarget = new KeyValuePair<float, int>(distance, targetLocations.IndexOf(target));
		}
		if (closestTarget.Value == -1)
		{
			currentTarget = null;
			return false;
		}
		currentTarget = targetLocations[closestTarget.Value];
		
		RaycastHit2D hit = Physics2D.Linecast(_transform.position, currentTarget.position, visionLayerMask);
		if (hit.collider.gameObject.GetComponentInParent<Player>() != null)
		{
			float lateralDistance = _transform.InverseTransformDirection(currentTarget.position - _transform.position).x;
			if (Mathf.Abs(lateralDistance) > attackRange) lastKnownTargetDirection = Mathf.Sign(_transform.InverseTransformDirection(currentTarget.position - _transform.position).x);
			else lastKnownTargetDirection = 0;
		}
		else
		{
			lastKnownTargetDirection = Mathf.Sign(view.eulerAngles.y - 90);
		}

		return true;
	}
	
	private bool CanReachTarget()
	{
		if (currentTarget == null) return false;
		if (_justJumped) return false;
		if (!canJump) return true;
		Vector3 targetDirection = _transform.InverseTransformDirection(currentTarget.position - _transform.position);
		if (targetDirection.y < 0) return true;
		return targetDirection.y <= 0.5f * Mathf.Abs(targetDirection.x);
	}
	
	public void JumpToTarget(Vector3 targetPosition)
	{
		Vector3 displacement = targetPosition - _transform.position;
		Vector3 jumpVelocity = displacement.normalized * jumpSpeed;
		float jumpDuration = displacement.magnitude / jumpSpeed;
		_justJumped = true;
		StartCoroutine(JumpTimer(jumpVelocity, jumpDuration*0.5f));
	}
	
	private IEnumerator JumpTimer(Vector3 jumpVelocity, float jumpDuration)
	{
		OnJump?.Invoke();
		string currentStateName = anim.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
		yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(currentStateName));
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		OnRun?.Invoke();
		terrainDetection.gameObject.SetActive(false);
		_rb.gravityScale = 3;
		_rb.velocity = jumpVelocity;
		if (jumpVelocity.x < 0) view.localRotation = Quaternion.identity;
		else view.localRotation = Quaternion.Euler(0, 180, 0);
		yield return new WaitForSeconds(jumpDuration);
		canJump = false;
		_justJumped = false;
		terrainDetection.gameObject.SetActive(true);
		// yield return new WaitForSeconds(Mathf.Max(jumpDuration - 0.3f, 0.1f));
		_rb.gravityScale = gravityScale;
		StartCoroutine(JumpCooldownTimer());
	}
	
	private IEnumerator JumpCooldownTimer()
	{
		yield return new WaitForSeconds(jumpCooldownDuration);
		canJump = true;
	}

	private void CooldownAttack()
	{
		StartCoroutine(CooldownAttackTimer());
	}
	
	private IEnumerator CooldownAttackTimer()
	{
		canAttack = false;
		yield return new WaitForSeconds(attackCooldown);
		canAttack = true;
	}

	private void Move(float lateralInput)
	{
		if (!terrainDetection.isActiveAndEnabled) return;
		if (!terrainDetection.isGrounded)
		{
			_rb.gravityScale = gravityScale;
			return;
		}
		if (_justJumped) return;
		_rb.gravityScale = 0;
		_rb.velocity = new Vector2(lateralInput * currentMoveSpeed * terrainDetection.mainNormal.y-terrainDetection.mainNormal.x*wallSuctionStrength,
			lateralInput * currentMoveSpeed * -terrainDetection.mainNormal.x-terrainDetection.mainNormal.y*wallSuctionStrength);
	}
    
    private void RotateToWall()
	{
		_rb.angularVelocity = Vector3.SignedAngle(_transform.TransformDirection(Vector3.up),terrainDetection.mainNormal, Vector3.forward)*rotateSpeed;
	}

	public void MovePerformed(float lateralInput)
	{
		if (_lateralMoveInput >= 0 && lateralInput < 0) view.localRotation = Quaternion.identity;
		else if (_lateralMoveInput <= 0 && lateralInput > 0) view.localRotation = Quaternion.Euler(0, 180, 0);

		_lateralMoveInput = lateralInput;
		if (!terrainDetection.isActiveAndEnabled) return;
        terrainDetection.lateralMoveInput = -Mathf.Abs(lateralInput);
    }

	public void MoveCancelled()
    {
        _lateralMoveInput = 0;
        if (!terrainDetection.isActiveAndEnabled) return;
        terrainDetection.lateralMoveInput = 0;
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
		StartCoroutine(AttackAnimation());
	}
	
	private IEnumerator AttackAnimation()
	{
		OnAttack?.Invoke();
		string currentStateName = anim.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
		yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(currentStateName));
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		CooldownAttack();
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

	public void ResumePerformed()
	{
		
	}

	public void ResumeCancelled()
	{
		
	}

	#region Weapons Testing

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

	public void DashPerformed()
	{
		
	}

	public void SprintPerformed()
	{
		
	}

	public void SprintCancelled()
	{
		
	}

	#endregion

	private enum SoldierScenario
	{
		SeesTarget = 0,
		AllTargetsDead = 1,
		TargetWithinRange = 2,
		CanReachTarget = 3,
		CanAttack = 4,
		CanPatrol = 5
	}
}
