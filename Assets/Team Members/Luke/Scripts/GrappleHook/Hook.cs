using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
	private Transform _transform;
	private Rigidbody2D _rb;
	private GrapplePlatform _grapplePlatform;

	public Action<Transform> OnHit;

	public void SetVelocity(float velocity)
	{
		_rb.velocity = transform.right * velocity;
	}
	
	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (_grapplePlatform == null) return;
		_transform.position = _grapplePlatform.transform.position;
	}

	private void OnDestroy()
	{
		if (_grapplePlatform != null) _grapplePlatform.isGrappled = false;
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		SetVelocity(0);
		OnHit?.Invoke(_transform);
		_grapplePlatform = col.gameObject.GetComponent<GrapplePlatform>();
		if (_grapplePlatform != null)
		{
			_grapplePlatform.isGrappled = true;
			_grapplePlatform.isMoving = true;
		}
	}
}
