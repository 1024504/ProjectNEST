using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class AlveriumBoss : EnemyBody, IControllable, ISense
{
	private Transform _transform;
	private Rigidbody2D _rb;
	public Transform tailAimTransform;
	public Transform projectileTransform;
	public float minTailAngle;
	public float maxTailAngle;
	private Vector3 _defaultTailAimDirection;
	private Quaternion _defaultRotation;
	public Transform view;
	
	public Transform _target;


	public float walkSpeed;
	public float runSpeed;
	[HideInInspector]
	public float currentMoveSpeed;
	private float _lateralInput;
	
	protected override void OnEnable()
	{
		base.OnEnable();
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_defaultTailAimDirection = tailAimTransform.forward;
		_defaultRotation = tailAimTransform.rotation;
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
		aWorldState.Set(BossScenario.CanAttack, false);
		aWorldState.Set(BossScenario.CanRun, false);
		aWorldState.Set(BossScenario.CanShoot, false);
		aWorldState.Set(BossScenario.TargetInMeleeRange, false);
		aWorldState.Set(BossScenario.TargetIsBelowHead, CheckTargetBelowHead());
		aWorldState.Set(BossScenario.TargetIsDead, false);
	}

	private void Move()
	{
		_rb.velocity = new Vector2(_lateralInput * currentMoveSpeed, _rb.velocity.y);
	}

	private void Aim()
	{
		if (_target == null) tailAimTransform.rotation = _defaultRotation;
		else
		{
			float angle = Vector3.SignedAngle(view.rotation*_defaultTailAimDirection, _target.position - tailAimTransform.position, Vector3.back);
			angle = Mathf.Clamp(angle, minTailAngle, maxTailAngle);
			if (view.rotation != Quaternion.identity) angle = -angle;
			tailAimTransform.localEulerAngles = new Vector3(angle, 90, 0);
		}
	}

	private void Shoot()
	{
		
	}

	private bool CheckTargetBelowHead() => _target != null && _target.position.y < _transform.position.y /*+ headheight*/;

	public void MovePerformed(float lateralInput)
    {
	    if (_lateralInput >= 0 && lateralInput < 0) view.localRotation = Quaternion.identity;
	    else if (_lateralInput <= 0 && lateralInput > 0) view.localRotation = Quaternion.Euler(0, 180, 0);
	    
	    _lateralInput = lateralInput;
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
	    CanAttack = 5
    }
}
