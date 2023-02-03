using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
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
	
	public void Jump()
	{
		if (!_foot.isGrounded) return;
		if (Vector2.Angle(Vector2.up, _foot.normal) > maxSlopeAngle) return;
		if (_coroutine != null) StopCoroutine(_coroutine);
		_foot.isGrounded = false;
		_rb.gravityScale = 0f;
		_rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
		_coroutine = StartCoroutine(JumpTimer());
	}

	public void CancelJump()
	{
		if (_foot.isGrounded) return;
		if (_coroutine != null) StopCoroutine(_coroutine);
		_rb.gravityScale = gravityScale;
		if (_rb.velocity.y > 0) _rb.velocity = new Vector2(_rb.velocity.x, 0);
	}

	private IEnumerator JumpTimer()
	{
		float decelerationDuration = jumpSpeed / gravityScale / Physics.gravity.magnitude;
		StartCoroutine(CeilingCheck());
		yield return new WaitForSeconds(jumpTime - decelerationDuration);
		_rb.gravityScale = gravityScale;
	}

	private IEnumerator CeilingCheck()
	{
		yield return new WaitUntil(() => _rb.velocity.y <= 0);
		if (_coroutine != null) CancelJump();
	}
}
