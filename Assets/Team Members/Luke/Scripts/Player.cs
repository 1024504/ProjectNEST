using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IControllable
{
	[Header("Design")]
	public float moveSpeed;
	public float maxSlopeAngle;
	public float jumpSpeed;
	public float jumpTime;
	public float gravityScale;
	
	[Header("Logic")]
	public float lateralMoveInput;
	private Coroutine _coroutine;

	private Transform _transform;
	private Rigidbody2D _rb;
	private PlayerFoot _foot;

	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_rb.gravityScale = gravityScale;
		_foot = GetComponentInChildren<PlayerFoot>();
	}

	private void FixedUpdate()
	{
		Move(lateralMoveInput);
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

	private IEnumerator JumpTimer()
	{
		float decelerationDuration = jumpSpeed / gravityScale / Physics.gravity.magnitude;
		yield return new WaitForSeconds(jumpTime - decelerationDuration);
		_rb.gravityScale = gravityScale;
	}

	public void MovePerformed(InputAction.CallbackContext context)
	{
		lateralMoveInput = Mathf.Ceil(context.ReadValue<Vector2>().x);
	}

	public void MoveCancelled(InputAction.CallbackContext context)
	{
		lateralMoveInput = 0;
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
		
	}

	public void Action1Cancelled(InputAction.CallbackContext context)
	{
		
	}
}
