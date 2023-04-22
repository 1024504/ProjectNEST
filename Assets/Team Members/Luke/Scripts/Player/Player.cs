using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using FMODUnity;
using Unity.Mathematics;


public class Player : MonoBehaviour, IGameplayControllable
{
	public GameObject aboveHeadUI;
	
	[Header("Movement")]
	[Tooltip("How fast the player moves along the floor.")]
	public float walkSpeed = 18f;
	[Tooltip("How fast the player moves while sprinting.")]
	public float sprintSpeed = 28f;
	
	private float _currentSpeed;
	private Coroutine _sprintCoroutine;
	
	[Tooltip("The highest slope the player can walk up, in degrees.")]
	public float maxSlopeAngle;
	[Tooltip("How fast the player jumps upwards.")]
	public float jumpSpeed;
	[Tooltip("How long after jumping, before the player starts falling naturally.")]
	public float jumpTime;
	[Tooltip("How fast the player falls downwards, does not affect jump time, slightly affects maximum jump height.")]
	public float gravityScale;
	[Tooltip("The force that is applied to the dash direction.")]
	public float dashForce;
	[SerializeField] private Transform _view;
	
	[Header("Grapple")]
	public bool grappleEnabled;
	[SerializeField] private Grapple _grapple;
	private bool _canGrapple = true;
	public float grappleVelocity = 35f;
	public float grappleRange = 20;
	public float grappleCooldown = 5;
	public float grapplePullStrength = 1;
	public float grapplePowerRatio = 1.6f;
	public float grappleDamping;
	public bool _isGrappled;
	private Transform _grappleHitTransform;
	
	private float _lateralMoveInput;
	private Vector2 _aimInput;
	private Coroutine _coroutine;
	private bool _justJumped = false;
	
	public bool doubleJumpEnabled = false;
	private bool _doubleJumped = false;

	[Header("Dash")]
	[Tooltip("How fast the player moves while dashing.")]
	public float dashVelocity;
	[Tooltip("How long the player dashes for in seconds.")]
	public float dashDuration;
	[Tooltip("How long the player has to wait before dashing again in seconds.")]
	public float dashCooldownDuration;
	private bool _isDashing = false;
	private bool _dashCoolingDown = false;

	[Header("Player AIM")]
	public Transform playerArms;
	public Transform lookTransform;
	public Transform sprintLookTransform;
	public float mouseAimSensitivity = 1;
	public float gamepadAimSensitivity = 10;
	private bool _isUsingGamepad;
	private Vector3 _localReticlePositionGamepad;
	
	[Header("Weapons")] 
	public WeaponBase currentWeapon;
	public Transform cameraTransform;
	[Range(0.25f,1)]
	public float gamePadReticleDistanceFraction = 0.8f;
	private float _reticleDistance;
	[Tooltip("The height and width of the camera's view, respectively. Updated by camera script.")]
	public Vector2 cameraSize;
	public float mouseReticleMargin = 1f;
	
	[Header("Inventory")]
	public List<WeaponBase> weaponsList;
	public int medkitCount;
	public int maxMedkit = 3;
	public int medKitHealLevel;
	public EventReference medkitEmote;

	private Transform _transform;
	private Rigidbody2D _rb;
	public PlayerTerrainDetection _terrainDetection;

	public delegate void MedKit();
	public event MedKit OnPickUp;
	
	public delegate void UpdateHealth();
	public event UpdateHealth OnChangeHealth;

	public delegate void HitGrapple();

	public event HitGrapple OnGrappleHit;

	public delegate void CurrentGunUI();

	public event CurrentGunUI OnGunSwitch;
	
	//UI events
	public Action OnReload;

	// Animation events
	public Action OnPlayerIdle;
	public Action OnPlayerWalkForwards;
	public Action OnPlayerWalkBackwards;
	public Action OnPlayerJump;
	public Action OnPlayerMidAirFalling;
	public Action OnPlayerLanding; // not sure about fading with walking/sprinting animation
	public Action OnPlayerDash;
	public Action OnPlayerSprint;
	public Action OnInteract;


	private void OnEnable()
	{
		DontDestroyOnLoad(gameObject);
		_currentSpeed = walkSpeed;
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_rb.gravityScale = gravityScale;
		_terrainDetection = GetComponentInChildren<PlayerTerrainDetection>();
		_grapple = GetComponentInChildren<Grapple>(true);
		_grapple.OnHit += GrappleHit;
		currentWeapon = weaponsList[0];
	}

	private void Start()
	{
		UIManager ui = UIManager.Instance;
		ui.player = this;
		ui.SubscribeToPlayerEvents();
		UpdateReticleDistance();
		GameManager.Instance.Resume();
	}

	private void OnDisable()
	{
		_grapple.OnHit -= GrappleHit;
	}

	private void FixedUpdate()
	{
		if(_isGrappled) GrappleMovement();
		Move(_lateralMoveInput);
		if (_isUsingGamepad) SlerpReticle();
		AimArms();
	}

	private void Move(float input)
	{
		if (_isDashing) return;
		
		if (_isGrappled)
		{
			_rb.velocity += new Vector2(input * _currentSpeed * Time.fixedDeltaTime, 0);
			return;
		}
		
		if (CheckIfOnSlope())
		{
			MoveDownSlope();
			return;
		}
		
		if (!_terrainDetection.isGrounded || _justJumped)
		{
			MoveInAir(input);
			return;
		}
		
		MoveOnGround(input);
	}

	private bool CheckIfOnSlope() => Vector2.Angle(Vector2.up, _terrainDetection.mainNormal) > maxSlopeAngle;

	private void MoveOnGround(float input)
	{
		_doubleJumped = false;
		_canGrapple = true;
		if (input < 0 && _terrainDetection.leftAngle < maxSlopeAngle ||
		    input > 0 && _terrainDetection.rightAngle < maxSlopeAngle)
		{
			if (_currentSpeed < sprintSpeed)
			{
				if (lookTransform.localPosition.x > 0 == input < 0) OnPlayerWalkBackwards?.Invoke();
				else OnPlayerWalkForwards?.Invoke();
			}
			else OnPlayerSprint?.Invoke();
			
			_rb.velocity = new Vector2(input * _currentSpeed * _terrainDetection.mainNormal.y,
				input * _currentSpeed * -_terrainDetection.mainNormal.x);
			return;
		}
		
		OnPlayerIdle?.Invoke();
		_rb.velocity = new Vector2(0, 0);
	}

	private void MoveDownSlope()
	{
		if (_doubleJumped) return;
		_rb.velocity = new Vector2(Mathf.Sign(_terrainDetection.mainNormal.x)*_terrainDetection.mainNormal.y*_currentSpeed, -Mathf.Abs(_terrainDetection.mainNormal.x)*_currentSpeed);
	}

	private void MoveInAir(float input)
	{
		OnPlayerMidAirFalling?.Invoke();
		_rb.velocity = new Vector2(input * _currentSpeed, _rb.velocity.y);
	}

	private void GrappleHit(Transform grappleHitTransform)
	{
		OnGrappleHit?.Invoke();
		_grappleHitTransform = grappleHitTransform;
		_isGrappled = true;
		_rb.gravityScale = gravityScale;
	}

	private void GrappleMovement()
	{
		Vector2 grappleDir = _grappleHitTransform.position - transform.position;
		_rb.AddForce(grappleDir.normalized * (Mathf.Pow(grappleDir.magnitude,grapplePowerRatio) * grapplePullStrength * Time.fixedDeltaTime), ForceMode2D.Impulse);
		Vector2 velocity = _rb.velocity;
		velocity -= velocity * grappleDamping * Time.fixedDeltaTime;
		_rb.velocity = velocity;
	}

	/// <summary>
	/// Triggers natural arc to finish jump on time if jump input isn't cancelled.
	/// </summary>
	/// <returns></returns>
	private IEnumerator JumpTimer()
	{
		float decelerationDuration = jumpSpeed / gravityScale / Physics.gravity.magnitude;
		for (float t = jumpTime - decelerationDuration; t > 0; t -= Time.fixedDeltaTime)
		{
			yield return new WaitForFixedUpdate();
		}
		_rb.gravityScale = gravityScale;
	}

	private IEnumerator JustJumped()
	{
		for (float t = 0.2f; t > 0; t -= Time.fixedDeltaTime)
		{
			yield return new WaitForFixedUpdate();
		}
		_justJumped = false;
	}

	private void AimArms()
	{
		// Clamp the mouse reticle within cameraBounds
		Vector3 cameraPosition = cameraTransform.position;
		Vector3 position = lookTransform.position;
		
		position = new Vector3(Mathf.Clamp(position.x, cameraPosition.x-cameraSize.x/2+mouseReticleMargin, cameraPosition.x+cameraSize.x/2-mouseReticleMargin),
			Mathf.Clamp(position.y, cameraPosition.y-cameraSize.y/2+mouseReticleMargin, cameraPosition.y+cameraSize.y/2-mouseReticleMargin));
		lookTransform.position = position;
		
		// Update position based on gun equipped so bullets go through crosshair
		Vector3 armPosition = playerArms.position;
		Vector3 offset = currentWeapon.gunBarrelTransform.position - armPosition;
		float barrelDistance = currentWeapon.gunBarrelTransform.InverseTransformPoint(position - offset).x;
		if (barrelDistance > 0.2f) position -= offset;
		else if (barrelDistance > 0) position -= offset * (barrelDistance / 0.2f);

		if (_currentSpeed < sprintSpeed)
		{
			playerArms.rotation = Quaternion.LookRotation(position - armPosition)*Quaternion.LookRotation(Vector3.left);
			if (lookTransform.localPosition.x >= 0) _view.localRotation = Quaternion.identity;
			else _view.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
		}
		else
		{
			if (_lateralMoveInput >= 0) _view.localRotation = Quaternion.identity;
			else _view.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
			playerArms.rotation = Quaternion.LookRotation(sprintLookTransform.position - armPosition)*Quaternion.LookRotation(Vector3.left);
		}
	}

	public void MovePerformed(float lateralInput)
	{
		_lateralMoveInput = lateralInput;
	}

	public void MoveCancelled()
	{
		_lateralMoveInput = 0;
	}

	public void AimPerformedMouse(Vector2 aimInput)
	{
		_isUsingGamepad = false;
		Vector2 aimRes = aimInput * (0.05f * mouseAimSensitivity);
		lookTransform.position += new Vector3(aimRes.x, aimRes.y, 0);
		AimArms();
	}
	
	public void AimPerformedGamepad(Vector2 input)
	{
		if (input.magnitude < 0.01f) return;
		_isUsingGamepad = true;
		Vector2 normalizedInput = input.normalized;
		_localReticlePositionGamepad = playerArms.position-_transform.position+new Vector3(normalizedInput.x * _reticleDistance, normalizedInput.y * _reticleDistance, 0);
		AimArms();
	}

	private void SlerpReticle()
	{
		Vector3 playerArmsPosition = playerArms.position;
		Vector3 lookPositionLocalToArms = lookTransform.position - playerArmsPosition;
		float angle = Vector3.SignedAngle(_localReticlePositionGamepad, lookPositionLocalToArms, Vector3.back);
		lookPositionLocalToArms = Vector3.Lerp(lookPositionLocalToArms, lookPositionLocalToArms.normalized * _reticleDistance, 0.1f);
		lookTransform.position = playerArmsPosition+lookPositionLocalToArms;
		lookTransform.RotateAround(playerArmsPosition, Vector3.forward, Mathf.Min(angle*0.1f));
	}

	public void AimCancelled()
	{
		_localReticlePositionGamepad = lookTransform.position - playerArms.position;
	}

	public void JumpPerformed()
	{
		if (_terrainDetection.isGrounded && Vector2.Angle(Vector2.up, _terrainDetection.mainNormal) <= maxSlopeAngle)
		{
			_justJumped = true;
			if (_coroutine != null) StopCoroutine(_coroutine);
			_rb.gravityScale = 0f;
			_rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
			StartCoroutine(JustJumped());
			_coroutine = StartCoroutine(JumpTimer());
		}
		else if (doubleJumpEnabled && !_doubleJumped)
		{
			_doubleJumped = true;
			_justJumped = true;
			if (_coroutine != null) StopCoroutine(_coroutine);
			_rb.gravityScale = 0f;
			_rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
			_coroutine = StartCoroutine(JumpTimer());
		}
		OnPlayerJump?.Invoke();
	}

	public void JumpCancelled()
	{
		
		if (!_justJumped) return;
		_justJumped = false;
		if (_terrainDetection.isGrounded) return;
		if (_coroutine != null) StopCoroutine(_coroutine);
		_rb.gravityScale = gravityScale;
		if (_rb.velocity.y > 0) _rb.velocity = new Vector2(_rb.velocity.x, 0);
	}

	public void ShootPerformed()
	{
		if (_isDashing) return;
		if (_currentSpeed >= sprintSpeed) return;
		WeaponBase weaponBase = GetComponentInChildren<WeaponBase>();
		weaponBase.isShooting = true;
		weaponBase.Shoot();
	}

	public void ShootCancelled()
	{
		WeaponBase weaponBase = GetComponentInChildren<WeaponBase>();
		weaponBase.isShooting = false;
	}

	public void Action1Performed()
	{
		if (!grappleEnabled) return;
		if (!_canGrapple) return;
		_canGrapple = false;
		_grapple.Shoot(grappleVelocity, grappleRange, grappleCooldown);
	}
	
	public void Action1Cancelled()
	{
		if (!grappleEnabled) return;
		_isGrappled = false;
		OnGrappleHit?.Invoke();
		_grapple.ResetGrapple();
	}

	public void Action2Performed()
	{
		WeaponBase weaponBase = GetComponentInChildren<WeaponBase>();
		weaponBase.Reload();
		if (weaponBase.isReloading)
		{
			OnReload?.Invoke();
		}
	}

	public void Action2Cancelled()
	{
		
	}

	public void Action3Performed()
	{
		OnInteract?.Invoke();
	}

	public void Action3Cancelled()
	{
		
	}

	public void PausePerformed()
	{
		GameManager.Instance.Pause();
	}

	public void PauseCancelled()
	{
		
	}

	#region Weapons Testing

	public void Weapon1Performed()
	{
		ChangeWeapon(0);
	}

	public void Weapon1Cancelled()
	{
		
	}
	
	public void Weapon2Performed()
	{
		ChangeWeapon(1);
	}
	
	public void Weapon2Cancelled()
	{
		
	}
	public void Weapon3Performed()
	{
		ChangeWeapon(2);
	}
	
	public void Weapon3Cancelled()
	{
		
	}

	public void MedKitPerformed()
	{
		HealthBase healthBase = gameObject.GetComponent<HealthBase>();
		if (healthBase.HealthLevel >= 100f) return;
		if (medkitCount <= 0) return;
		healthBase.HealthLevel += medKitHealLevel;
		RuntimeManager.PlayOneShot(medkitEmote);
		medkitCount--;
		OnPickUp?.Invoke();
		OnChangeHealth?.Invoke();		
	}

	public void MedKitCancelled()
	{
		
	}
	

	public void DashPerformed()
	{
		// last a fixed number of frames

		if (!_terrainDetection.isGrounded) return;
		if (_dashCoolingDown) return;
		
		_isDashing = true;
		_dashCoolingDown = true;
		ShootCancelled();

		StartCoroutine(Dash());
	}
	
	private IEnumerator Dash()
	{
		OnPlayerDash?.Invoke();
		
		float counter = 0;
		
		float dashInput = _lateralMoveInput;
		if (dashInput == 0) dashInput = _view.right.x;
		
		while (counter < dashDuration)
		{
			counter += Time.fixedDeltaTime;
			_rb.velocity = new Vector2(dashInput * dashVelocity, _rb.velocity.y);
			yield return new WaitForFixedUpdate();
		}
		
		_isDashing = false;
		StartCoroutine(DashCooldown());
	}

	private IEnumerator DashCooldown()
	{
		yield return new WaitForSeconds(dashCooldownDuration);
		_dashCoolingDown = false;
	}

	public void DashHeld()
	{
		//Sprint
		if (_sprintCoroutine != null) StopCoroutine(_sprintCoroutine);
		_sprintCoroutine = StartCoroutine(SprintJumpCheck());
	}

	private IEnumerator SprintJumpCheck()
	{
		while (!_terrainDetection.isGrounded)
		{
			yield return new WaitForEndOfFrame();
		}
		ShootCancelled();
		_currentSpeed = sprintSpeed;
	}

	public void DashCancelled()
	{
		//after dash. Collisions etc. 
		if (_sprintCoroutine != null) StopCoroutine(_sprintCoroutine);
		_currentSpeed = walkSpeed;
	}

	private void ChangeWeapon(int weaponNo)
	{
		WeaponBase weaponBase = GetComponentInChildren<WeaponBase>();
		if (weaponBase.isReloading) return;

		if (weaponsList[weaponNo].gameObject.activeSelf) return;
		
		for (int i = 0; i < weaponsList.Count; i++)
		{
			if (i == weaponNo)
			{
				weaponsList[i].gameObject.SetActive(true);
				currentWeapon = weaponsList[i];
				OnGunSwitch?.Invoke();
			}
			else
			{
				weaponsList[i].gameObject.SetActive(false);
			}
		}
		
		weaponBase = GetComponentInChildren<WeaponBase>();
		cameraTransform.GetComponent<CameraTracker>().CameraSize = weaponBase.cameraSize;
		UpdateReticleDistance();
	}
	
	private void UpdateReticleDistance() => _reticleDistance = gamePadReticleDistanceFraction*currentWeapon.bulletRange;

	#endregion

	#region ItemPickupTest

	public void PickUp()
	{
		if (medkitCount == maxMedkit) return;
		medkitCount++;
		OnPickUp?.Invoke();
	}

	#endregion
}