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
	[Header("Design")]
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
	public bool grappleEnabled;
	public float grappleShootSpeed;
	public float grappleRange;
	public float grappleReturnSpeed;
	public float grapplePullSpeed;
	
	private float _lateralMoveInput;
	private Vector2 _aimInput;
	private Coroutine _coroutine;

	[Header("Weapons")]
	//public GameObject equippedWeapon;
	//public GameObject bulletPrefab;
	//public Transform barrelTransform;
	public List<GameObject> weaponsList;
	//public List<GameObject> bulletList;
	//public List<Transform> barrelTransforms;
	public Transform playerArms;
	public Transform lookTransform;
	public float mouseAimSensitivity = 1;
	public Vector3 mousePos;
	

	private Transform _transform;
	private Rigidbody2D _rb;
	private TerrainCollider _terrainCollider;
	[SerializeField] private Grapple _grapple;
	
	//UI controls
	[SerializeField] private UIManager _uiManager;

	public int medkitCount;
	public int maxMedkit = 3;
	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_rb.gravityScale = gravityScale;
		_terrainCollider = GetComponentInChildren<TerrainCollider>();
		_grapple = GetComponentInChildren<Grapple>(true);
	}

	private void FixedUpdate()
	{
		Move(_lateralMoveInput);
	}
	
	private void Move(float input)
	{
		if (!_terrainCollider.isGrounded)
		{
			_rb.velocity = new Vector2(input * moveSpeed, _rb.velocity.y);
		}
		else if (Vector2.Angle(Vector2.up, _terrainCollider.normal) <= maxSlopeAngle)
		{
			_rb.velocity = new Vector2(input * moveSpeed * _terrainCollider.normal.y,
				input * moveSpeed * -_terrainCollider.normal.x);
		}
		else
		{
			_rb.velocity = new Vector2(Mathf.Sign(_terrainCollider.normal.x)*_terrainCollider.normal.y*moveSpeed, -Mathf.Abs(_terrainCollider.normal.x)*moveSpeed);
		}
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

	public void MovePerformed(float lateralInput)
	{
		_lateralMoveInput = lateralInput;
		
		//turn the player when moving left and right tests
		/*if (_lateralMoveInput < 0)
		{
			gameObject.transform.Rotate(new Vector3(0, 180, 0));
		}
		else if (_lateralMoveInput > 0)
		{
			gameObject.transform.Rotate(new Vector3(0, -180, 0));
		}*/
	}

	public void MoveCancelled()
	{
		_lateralMoveInput = 0;
	}

	public void AimPerformedMouse(Vector2 aimInput)
	{
		Vector2 aimRes = aimInput * (0.05f * mouseAimSensitivity);
		Vector3 position = _transform.position;
		Vector3 lookPosition = lookTransform.position;
		lookPosition += new Vector3(aimRes.x, aimRes.y, 0);
		
		lookPosition = position + 5*Vector3.Normalize(lookPosition-position);
		lookTransform.position = lookPosition;

		playerArms.LookAt(lookPosition);
	}

	// Someone with XBox controller please test this :)
	public void AimPerformedGamepad(Vector2 input)
	{
		Vector3 position = _transform.position;
		playerArms.LookAt(position + new Vector3(input.x*5, input.y*5, 0));
	}

	public void AimCancelled()
	{
		
	}

	public void JumpPerformed()
	{
		if (!_terrainCollider.isGrounded) return;
		if (Vector2.Angle(Vector2.up, _terrainCollider.normal) > maxSlopeAngle) return;
		if (_coroutine != null) StopCoroutine(_coroutine);
		_terrainCollider.isGrounded = false;
		_rb.gravityScale = 0f;
		_rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
		_coroutine = StartCoroutine(JumpTimer());
	}

	public void JumpCancelled()
	{
		if (_terrainCollider.isGrounded) return;
		if (_coroutine != null) StopCoroutine(_coroutine);
		_rb.gravityScale = gravityScale;
		if (_rb.velocity.y > 0) _rb.velocity = new Vector2(_rb.velocity.x, 0);
	}

	public void ShootPerformed()
	{
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
		// _grapple.rend.enabled = true;
	}
	
	public void Action1Cancelled()
	{
		
	}

	public void Action2Performed()
	{
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
			_uiManager.Pause();
		}
		else if (GameManager.Instance.gamePaused)
		{
			_uiManager.ResumeButton();
		}
		
	}

	public void PauseCancelled()
	{
		
	}

	#region Weapons Testing

	public void Weapon1Performed()
	{
		WeaponBase weaponBase = GetComponentInChildren<WeaponBase>();
		if (weaponBase.isReloading) return;
		weaponsList[0].SetActive(true);
		weaponsList[1].SetActive(false);
		weaponsList[2].SetActive(false);
	}

	public void Weapon1Cancelled()
	{
		
	}
	
	public void Weapon2Performed()
	{
		WeaponBase weaponBase = GetComponentInChildren<WeaponBase>();
		if (weaponBase.isReloading) return;
		weaponsList[0].SetActive(false);
		weaponsList[1].SetActive(true);
		weaponsList[2].SetActive(false);
	}
	
	public void Weapon2Cancelled()
	{
		
	}
	public void Weapon3Performed()
	{
		WeaponBase weaponBase = GetComponentInChildren<WeaponBase>();
		if (weaponBase.isReloading) return;
		weaponsList[0].SetActive(false);
		weaponsList[1].SetActive(false);
		weaponsList[2].SetActive(true);
	}
	
	public void Weapon3Cancelled()
	{
		
	}

	#endregion

	#region ItemPickupTest

	public void PickUp()
	{
		if (medkitCount == maxMedkit) return;
		medkitCount++;
	}

	#endregion
}
