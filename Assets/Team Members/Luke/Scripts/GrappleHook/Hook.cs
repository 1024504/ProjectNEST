using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
	private Rigidbody2D _rb;

	public Action<Vector3> OnHit;

	public void SetVelocity(float velocity)
	{
		_rb.velocity = transform.right * velocity;
	}
	
	private void OnEnable()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		SetVelocity(0);
		OnHit?.Invoke(transform.position);
	}
}
