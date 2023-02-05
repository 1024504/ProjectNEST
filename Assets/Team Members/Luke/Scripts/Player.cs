using System;
using System.Collections;
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

	private Transform _transform;
	private Rigidbody2D _rb;
	private PlayerFoot _foot;
	[SerializeField] private Grapple _grapple;

	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_rb.gravityScale = gravityScale;
		_foot = GetComponentInChildren<PlayerFoot>();
		_grapple = GetComponentInChildren<Grapple>(true);
	}

	private void FixedUpdate()
	{
		Move(_lateralMoveInput);
	}
	
	private void Move(float input)
	{
		if (!_foot.isGrounded)
		{
			_rb.velocity = new Vector2(input * moveSpeed, _rb.velocity.y);
		}
		else if (Vector2.Angle(Vector2.up, _foot.normal) <= maxSlopeAngle)
		{
			_rb.velocity = new Vector2(input * moveSpeed * _foot.normal.y,
				input * moveSpeed * -_foot.normal.x);
		}
		else
		{
			_rb.velocity = new Vector2(Mathf.Sign(_foot.normal.x)*_foot.normal.y*moveSpeed, -Mathf.Abs(_foot.normal.x)*moveSpeed);
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

	public void MovePerformed(InputAction.CallbackContext context)
	{
		_lateralMoveInput = Mathf.Ceil(context.ReadValue<Vector2>().x);
	}

	public void MoveCancelled(InputAction.CallbackContext context)
	{
		_lateralMoveInput = 0;
	}
	
	public void JumpPerformed(InputAction.CallbackContext context)
	{
		if (!_foot.isGrounded) return;
		if (Vector2.Angle(Vector2.up, _foot.normal) > maxSlopeAngle) return;
		if (_coroutine != null) StopCoroutine(_coroutine);
		_foot.isGrounded = false;
		_rb.gravityScale = 0f;
		_rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
		_coroutine = StartCoroutine(JumpTimer());
	}

	public void JumpCancelled(InputAction.CallbackContext context)
	{
		if (_foot.isGrounded) return;
		if (_coroutine != null) StopCoroutine(_coroutine);
		_rb.gravityScale = gravityScale;
		if (_rb.velocity.y > 0) _rb.velocity = new Vector2(_rb.velocity.x, 0);
	}

	public void ShootPerformed(InputAction.CallbackContext context)
	{
		
	}

	public void ShootCancelled(InputAction.CallbackContext context)
	{
		
	}

	public void Action1Performed(InputAction.CallbackContext context)
	{
		if (!grappleEnabled) return;
		// _grapple.rend.enabled = true;
	}

	public void Action1Cancelled(InputAction.CallbackContext context)
	{
		
	}
}
