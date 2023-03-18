using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour, IControllable
{
	[Header("Movement")]
	[Tooltip("How fast the player moves along the floor.")]
	public float moveSpeed;
	[Tooltip("The highest slope the player can walk up, in degrees.")]
	public float maxSlopeAngle;
	[Tooltip("How fast the player jumps upwards.")]
	public float jumpSpeed;
	[Tooltip("How long after jumping, before the player starts falling naturally.")]
	public float jumpTime;
	[Tooltip("How fast the player falls downwards, does not affect jump time, slightly affects maximum jump height.")]
	public float gravityScale;
	
	[Header("Grapple")]
	public bool grappleEnabled;
	private bool _canGrapple = true;
	public float grappleVelocity = 35f;
	public float grappleRange = 20;
	public float grappleCooldown = 5;
	public float grapplePullStrength = 1;
	public float grapplePowerRatio = 1.6f;
	public float grappleDamping;
	private bool _isGrappled;
	private Vector3 _grapplePoint;
	
	private float _lateralMoveInput;
	private Vector2 _aimInput;
	private Coroutine _coroutine;
	private bool _isJumping = false;
	
	public bool doubleJumpEnabled = false;
	private bool _doubleJumped = false;

	[Header("Weapons")]
	public Transform cameraTransform;
	public float gamePadReticleDistance = 5f;
	[Tooltip("The height and width of the camera's view, respectively. Updated by camera script.")]
	public Vector2 cameraSize;
	public float mouseReticleMargin = 1f;
	//public GameObject equippedWeapon;
	//public GameObject bulletPrefab;
	//public Transform barrelTransform;
	public List<GameObject> weaponsList;
	//public List<GameObject> bulletList;
	//public List<Transform> barrelTransforms;
	public Transform playerArms;
	public Transform lookTransform;
	public float mouseAimSensitivity = 1;

	private Transform _transform;
	[SerializeField] private Transform _view;
	private Rigidbody2D _rb;
	private PlayerTerrainDetection _terrainDetection;
	[SerializeField] private Grapple _grapple;

	public int medkitCount;
	public int maxMedkit = 3;
	public delegate void MedKit();
	public event MedKit OnPickUp;
	
	public delegate void UpdateHealth();
	public event UpdateHealth OnChangeHealth;
	
	

	public Action OnPlayerIdle;
	public Action OnPlayerWalk;
	public Action OnPlayerJump;

	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_rb.gravityScale = gravityScale;
		_terrainDetection = GetComponentInChildren<PlayerTerrainDetection>();
		_grapple = GetComponentInChildren<Grapple>(true);
		_grapple.OnHit += GrappleHit;
	}

	private void OnDisable()
	{
		_grapple.OnHit -= GrappleHit;
	}

	private void FixedUpdate()
	{
		if(_isGrappled) GrappleMovement();
		Move(_lateralMoveInput);
	}

	private void Update()
	{
		AimArms();
	}

	private void Move(float input)
	{
		if (GameManager.Instance.gamePaused) return;

		if (_isGrappled)
		{
			_rb.velocity += new Vector2(input * moveSpeed * Time.fixedDeltaTime, 0);
			return;
		}
		
		if (CheckIfOnSlope())
		{
			MoveDownSlope();
			return;
		}
		
		if (!_terrainDetection.isGrounded || _isJumping)
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
			OnPlayerWalk?.Invoke();
			_rb.velocity = new Vector2(input * moveSpeed * _terrainDetection.mainNormal.y,
				input * moveSpeed * -_terrainDetection.mainNormal.x);
			return;
		}
		
		OnPlayerIdle?.Invoke();
		_rb.velocity = new Vector2(0, 0);
	}

	private void MoveDownSlope()
	{
		if (_doubleJumped) return;
		_rb.velocity = new Vector2(Mathf.Sign(_terrainDetection.mainNormal.x)*_terrainDetection.mainNormal.y*moveSpeed, -Mathf.Abs(_terrainDetection.mainNormal.x)*moveSpeed);
	}

	private void MoveInAir(float input)
	{
		OnPlayerJump?.Invoke();
		_rb.velocity = new Vector2(input * moveSpeed, _rb.velocity.y);
	}

	private void GrappleHit(Vector3 grapplePoint)
	{
		_grapplePoint = grapplePoint;
		_isGrappled = true;
		_rb.gravityScale = gravityScale;
	}

	private void GrappleMovement()
	{
		Vector2 grappleDir = _grapplePoint - transform.position;
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
		yield return new WaitForSeconds(jumpTime - decelerationDuration);
		_rb.gravityScale = gravityScale;
	}
	
	private void AimArms()
	{
		// Clamp the mouse reticle within cameraBounds
		Vector3 cameraPosition = cameraTransform.position;
		Vector3 position = lookTransform.position;
		
		position = new Vector3(Mathf.Clamp(position.x, cameraPosition.x-cameraSize.x/2+mouseReticleMargin, cameraPosition.x+cameraSize.x/2-mouseReticleMargin),
			Mathf.Clamp(position.y, cameraPosition.y-cameraSize.y/2+mouseReticleMargin, cameraPosition.y+cameraSize.y/2-mouseReticleMargin));
		lookTransform.position = position;

		playerArms.LookAt(position);
		
		if (lookTransform.localPosition.x >= 0)
		{
			_view.localRotation = Quaternion.identity;
		}
		else
		{
			_view.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
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
		if (GameManager.Instance.gamePaused) return;
		Vector2 aimRes = aimInput * (0.05f * mouseAimSensitivity);
		lookTransform.position += new Vector3(aimRes.x, aimRes.y, 0);
	}

	// Someone with XBox controller please test this :)
	public void AimPerformedGamepad(Vector2 input)
	{
		if (GameManager.Instance.gamePaused) return;
		Vector3 position = _transform.position;

		lookTransform.position = position + new Vector3(input.x * gamePadReticleDistance, input.y * gamePadReticleDistance, 0);
	}

	public void AimCancelled()
	{
		
	}

	public void JumpPerformed()
	{
		if (GameManager.Instance.gamePaused) return;
		if (_terrainDetection.isGrounded && Vector2.Angle(Vector2.up, _terrainDetection.mainNormal) <= maxSlopeAngle)
		{
			_isJumping = true;
			if (_coroutine != null) StopCoroutine(_coroutine);
			_rb.gravityScale = 0f;
			_rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
			_coroutine = StartCoroutine(JumpTimer());
		}
		else if (doubleJumpEnabled && !_doubleJumped)
		{
			_doubleJumped = true;
			_isJumping = true;
			if (_coroutine != null) StopCoroutine(_coroutine);
			_rb.gravityScale = 0f;
			_rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
			_coroutine = StartCoroutine(JumpTimer());
		}
	}

	public void JumpCancelled()
	{
		if (GameManager.Instance.gamePaused) return;
		
		if (!_isJumping) return;
		_isJumping = false;
		if (_terrainDetection.isGrounded) return;
		if (_coroutine != null) StopCoroutine(_coroutine);
		_rb.gravityScale = gravityScale;
		if (_rb.velocity.y > 0) _rb.velocity = new Vector2(_rb.velocity.x, 0);
	}

	public void ShootPerformed()
	{
		if (GameManager.Instance.gamePaused) return;
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
		_grapple.ResetGrapple();
	}

	public void Action2Performed()
	{
		if (GameManager.Instance.gamePaused) return;
		WeaponBase weaponBase = GetComponentInChildren<WeaponBase>();
		weaponBase.Reload();
	}

	public void Action2Cancelled()
	{
		
	}

	public void PausePerformed()
	{
		if (GameManager.Instance.gamePaused == false)
		{
			GameManager.Instance._uiManager.Pause();
		}
		else if (GameManager.Instance.gamePaused)
		{
			GameManager.Instance._uiManager.ResumeButton();
		}
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
		healthBase.HealthLevel += 10f;
		medkitCount--;
		OnPickUp?.Invoke();
		OnChangeHealth?.Invoke();
	}

	public void MedKitCancelled()
	{
		
	}

	private void ChangeWeapon(int weaponNo)
	{
		if (GameManager.Instance.gamePaused) return;
		WeaponBase weaponBase = GetComponentInChildren<WeaponBase>();
		if (weaponBase.isReloading) return;

		for (int i = 0; i < weaponsList.Count; i++)
		{
			if (i == weaponNo) weaponsList[i].SetActive(true);
			else weaponsList[i].SetActive(false);
		}
		
		weaponBase = GetComponentInChildren<WeaponBase>();
		cameraTransform.GetComponent<CameraTracker>().CameraSize = weaponBase.cameraSize;
	}

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