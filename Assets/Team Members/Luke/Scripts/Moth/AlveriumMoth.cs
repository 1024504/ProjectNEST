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
	public float idealDistanceFromTarget = 16f;

	private float _lateralMoveInput;
	private float _verticalMoveInput;
	private bool _shootCoolingDown;
	private bool _canPatrol;
	// private bool _canHitTarget = false;

	[SerializeField] private Transform view;
	[SerializeField] private Transform tailTransform;
	private Transform _currentTarget;
	public Transform aimTransform;
	[SerializeField] private Transform projectileTransform;
	
	public List<Transform> targetLocations;
	public float memoryDuration = 5f;
	
	public Vector3 localDefaultAimPosition;

	public GameObject projectilePrefab;
	public SpriteRenderer localProjectileRenderer;
	
	private Transform _transform;
	private Rigidbody2D _rb;
	public Animator anim;
	public Action OnIdle;
	public Action OnMoveBurst;
	public Action OnMoveConstant;
	public Action OnAttackBuildup;
	public Action OnAttackShoot;
	public LayerMask visionLayerMask;

	protected override void OnEnable()
	{
		base.OnEnable();
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
		aWorldState.Set(MothScenario.HasTarget, ChooseTarget());
		aWorldState.Set(MothScenario.CanHitTarget, CheckVerticalDistanceToTarget() < 0);
		aWorldState.Set(MothScenario.AllTargetsDead, false);
	}

	private bool ChooseTarget()
	{
		if (targetLocations.Count == 0)
		{
			_currentTarget = null;
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
			_currentTarget = null;
			return false;
		}
		
		_currentTarget = targetLocations[closestTarget.Value];
		return true;
	}

	public float CheckVerticalDistanceToTarget()
	{
		if (_currentTarget == null) return Mathf.Infinity;
		return _currentTarget.position.y - tailTransform.position.y;
	}
	
	public float CheckLateralDistanceToTarget() => _currentTarget.position.x - tailTransform.position.x;

	private void Move(float lateralInput, float verticalInput = 0)
	{
		Vector2 input = new Vector2(lateralInput, verticalInput);
		input = input.normalized;

		_rb.velocity = input * moveSpeed;
	}

	private void Aim()
	{
		if (_currentTarget == null) aimTransform.localPosition = localDefaultAimPosition;
		else aimTransform.position = _currentTarget.position;
		Vector3 aimPosition = aimTransform.position;
		Vector3 aimLocalPosition = aimTransform.localPosition;

		aimPosition = new Vector3(aimPosition.x, Mathf.Min(aimPosition.y, tailTransform.position.y), aimPosition.z);
		
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
	    _verticalMoveInput = 1;
    }

    public void JumpCancelled()
    {
	    _verticalMoveInput = 0;
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

    public void DashPerformed()
    {
	    
    }

    public void SprintPerformed()
    {
	    _verticalMoveInput = -1;
    }

    public void SprintCancelled()
    {
	    _verticalMoveInput = 0;
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
