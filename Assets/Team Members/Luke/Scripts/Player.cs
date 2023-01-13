using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Design")]
	public float moveSpeed;
	public float jumpSpeed;
	public float jumpTime;
	public float gravityScale; 
	public LayerMask groundLayerMask;
	[SerializeField] private Vector3 groundCheckCentre = new (0, -0.55f, 0);
	[SerializeField] private float groundCheckRadius = 0.5f;
	
	[Header("Logic")]
	public float lateralMoveInput;
	private bool _isJumping;
	private Coroutine _coroutine;

	private Transform _transform;
	private Rigidbody2D _rb;

	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_rb.gravityScale = gravityScale;
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

	private bool IsOnGround => Physics2D.OverlapCircle(_transform.position + groundCheckCentre, groundCheckRadius, groundLayerMask);
}
