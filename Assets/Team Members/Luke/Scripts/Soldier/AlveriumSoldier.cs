using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using JetBrains.Annotations;
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

	[SerializeField] private Transform view;

	public List<Transform> targetLocations;
	public Transform currentTarget;
	public float lastKnownTargetDirection;

    private SoldierTerrainDetection _terrainDetection;

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
	
	// private bool _isJumping = false;
	private bool _canPatrol = true;
	public bool targetWithinRange = false;
	public bool canAttack = true;

	private float _lateralMoveInput;

	public float rotateSpeed = 1;

	private enum Joints
	{
		Body1,
		Body2,
		Body3,
		Arm1,
		Arm2,
		Arm3,
		Head1,
		Head2,
		Head3,
		FrontLeg1,
		FrontLeg2,
		BackLeg1,
		BackLeg2
	}
	
	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
        _terrainDetection = (SoldierTerrainDetection)GetComponentInChildren<TerrainDetection>();
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
		aWorldState.Set(SoldierScenario.CanReachTarget, true);
		aWorldState.Set(SoldierScenario.CanAttack, canAttack);
		aWorldState.Set(SoldierScenario.AllTargetsDead, false);
	}

	public IEnumerator CanPatrolTimer(float delay)
	{
		yield return new WaitForSeconds(delay);
		_canPatrol = !_canPatrol;
	}

	[SerializeField] private LayerMask visionLayerMask = 520;

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
			RaycastHit2D hit = Physics2D.Linecast(_transform.position, target.position, visionLayerMask);
			if (hit.collider.gameObject.GetComponentInParent<Player>() == null) continue;
			float distance = Vector2.Distance(_transform.position, target.position);
			if (distance < closestTarget.Key) closestTarget = new KeyValuePair<float, int>(distance, targetLocations.IndexOf(target));
		}

		if (closestTarget.Value == -1)
		{
			currentTarget = null;
			return false;
		}
		
		currentTarget = targetLocations[closestTarget.Value];

		float lateralDistance = _transform.InverseTransformDirection(currentTarget.position - _transform.position).x;
		if (Mathf.Abs(lateralDistance) > attackRange) lastKnownTargetDirection = Mathf.Sign(_transform.InverseTransformDirection(currentTarget.position - _transform.position).x);
		else lastKnownTargetDirection = 0;
		return true;
	}

	public void CooldownAttack()
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
        if (!_terrainDetection.isGrounded)
        {
	        _rb.gravityScale = gravityScale;
	        _rb.velocity = new Vector2(lateralInput * currentMoveSpeed, _rb.velocity.y);
        }
        else
        {
	        _rb.gravityScale = 0;
	        _rb.velocity = new Vector2(lateralInput * currentMoveSpeed * _terrainDetection.mainNormal.y-_terrainDetection.mainNormal.x*wallSuctionStrength,
                lateralInput * currentMoveSpeed * -_terrainDetection.mainNormal.x-_terrainDetection.mainNormal.y*wallSuctionStrength);
        }
    }
    
    private void RotateToWall()
	{
		_rb.angularVelocity = Vector3.SignedAngle(_transform.TransformDirection(Vector3.up),_terrainDetection.mainNormal, Vector3.forward)*rotateSpeed;
	}

	public void MovePerformed(float lateralInput)
	{
		if (_lateralMoveInput >= 0 && lateralInput < 0)
		{
			view.localRotation = Quaternion.identity;
		}
		else if (_lateralMoveInput <= 0 && lateralInput > 0)
		{
			view.localRotation = Quaternion.Euler(0, 180, 0);
		}

		_lateralMoveInput = lateralInput;
        _terrainDetection.lateralMoveInput = -Mathf.Abs(lateralInput);
    }

	public void MoveCancelled()
    {
        _lateralMoveInput = 0;
        _terrainDetection.lateralMoveInput = 0;
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
        StartCoroutine(JumpTimer());
    }

    private IEnumerator JumpTimer()
    {
        _rb.gravityScale = 0;
        _rb.velocity += _terrainDetection.mainNormal * jumpSpeed;
        yield return new WaitForSeconds(1f);
        
        _rb.gravityScale = gravityScale;
    }

	public void JumpCancelled()
	{
		
	}

	public void ShootPerformed()
	{
		
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
		
	}

	public void DashCancelled()
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
