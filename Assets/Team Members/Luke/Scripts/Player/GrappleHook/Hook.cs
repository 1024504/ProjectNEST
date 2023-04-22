using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
	private Transform _transform;
	private Rigidbody2D _rb;
	[HideInInspector]
	public GrapplePlatform grapplePlatform;
	[SerializeField] private ParticleSystem particleSystemSparks;
	private CircleCollider2D _collider;

	public Action<Transform> OnHit;

	public void SetVelocity(float velocity)
	{
		_rb.velocity = transform.right * velocity;
	}
	
	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_collider = GetComponent<CircleCollider2D>();
	}

	private void FixedUpdate()
	{
		if (grapplePlatform == null) return;
		_transform.position = grapplePlatform.transform.position + _transform.TransformDirection(new Vector3(-_collider.offset.x, 0,0));
	}

	private void OnDestroy()
	{
		if (grapplePlatform != null) grapplePlatform.isGrappled = false;
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		SetVelocity(0);
		OnHit?.Invoke(_transform);
		Instantiate(particleSystemSparks, transform.position, Quaternion.identity);
	}

	public void ConnectGrapplePlatform(GrapplePlatform platform)
	{
		SetVelocity(0);
		OnHit?.Invoke(_transform);
		grapplePlatform = platform;
		grapplePlatform.isGrappled = true;
		grapplePlatform.isMoving = true;
		Instantiate(particleSystemSparks, transform.position, Quaternion.identity);
	}
}
