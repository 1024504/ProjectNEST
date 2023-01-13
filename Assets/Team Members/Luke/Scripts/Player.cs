using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Design")]
	public float moveSpeed;
	public float jumpSpeed;
	public float jumpTime;
	public float gravityScale; 
	public LayerMask groundLayerMask;

	[Header("Logic")]
	public float lateralMoveInput;
	private bool _isJumping;
	private float _distanceToGround;
	private Coroutine _coroutine;

	private Transform _transform;
	private Rigidbody2D _rb;

	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_rb.gravityScale = gravityScale;
		_distanceToGround = GetComponent<Collider2D>().bounds.extents.y;
	}

	private void FixedUpdate()
	{
		Move(lateralMoveInput);
	}
	
	private void Move(float input)
	{
		_rb.velocity = new Vector2(Mathf.Ceil(input) * moveSpeed, _rb.velocity.y);
	}
	
	public void Jump()
	{
		if (!IsOnGround) return;
		if (_coroutine != null) StopCoroutine(_coroutine);
		_isJumping = true;
		_rb.gravityScale = 0f;
		_rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
		_coroutine = StartCoroutine(JumpTimer());
	}

	public void CancelJump()
	{
		if (!_isJumping) return;
		if (_coroutine != null) StopCoroutine(_coroutine);
		_isJumping = false;
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

	private bool IsOnGround => Physics2D.Raycast(_transform.position, Vector2.down, _distanceToGround + 0.1f, groundLayerMask);
}
