using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
	public GameObject bulletPrefab;
	public Transform barrelTransform;
	public List<GameObject> weaponsList;
	public List<GameObject> bulletList;
	public List<Transform> barrelTransforms;

	//public Camera camera;

	private Transform _transform;
	private Rigidbody2D _rb;
	private TerrainCollider _terrainCollider;
	[SerializeField] private Grapple _grapple;
	

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

		Vector3 gunPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (gunPos.x < transform.position.x)
		{
			gameObject.transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
		}
		else
		{
			gameObject.transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
		}
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
	}

	public void MoveCancelled()
	{
		_lateralMoveInput = 0;
	}

	public void AimPerformed(Vector2 aimInput)
	{
		
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
		Instantiate(bulletPrefab, barrelTransform.position, transform.rotation);
	}

	public void ShootCancelled()
	{
		
	}

	public void Action1Performed()
	{
		if (!grappleEnabled) return;
		// _grapple.rend.enabled = true;
	}
	
	public void Action1Cancelled()
	{
		
	}

	#region Weapons Testing

	public void Weapon1Performed()
	{
		SetBulletPrefab(bulletList[0]);
		SetBarrelTransform(barrelTransforms[0]);
		weaponsList[0].SetActive(true);
		weaponsList[1].SetActive(false);
		weaponsList[2].SetActive(false);
		//equippedWeapon = weaponsList[0];
	}

	public void Weapon1Cancelled()
	{
		
	}
	
	public void Weapon2Performed()
	{
		SetBulletPrefab(bulletList[1]);
		SetBarrelTransform(barrelTransforms[1]);
		weaponsList[0].SetActive(false);
		weaponsList[1].SetActive(true);
		weaponsList[2].SetActive(false);
		//equippedWeapon = weaponsList[1];
	}
	
	public void Weapon2Cancelled()
	{
		
	}
	public void Weapon3Performed()
	{
		SetBulletPrefab(bulletList[2]);
		SetBarrelTransform(barrelTransforms[2]);
		weaponsList[0].SetActive(false);
		weaponsList[1].SetActive(false);
		weaponsList[2].SetActive(true);
		//equippedWeapon = weaponsList[2];
	}
	
	public void Weapon3Cancelled()
	{
		
	}
	
	private void SetBarrelTransform(Transform newTransform)
	{
		barrelTransform = newTransform;
	}
	private void SetBulletPrefab(GameObject newBullet)
	{
		bulletPrefab = newBullet;
	}
	
	#endregion
	
	
	
}
