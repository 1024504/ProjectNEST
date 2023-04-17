using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothProjectile : MonoBehaviour
{
	public float velocity = 25f;
	public float lifetimePostCollision = 3f;
	public float damage = 10f;
	
	private Transform _transform;
	private Rigidbody2D _rb;
	private PolygonCollider2D _collider;
	private Transform _collisionHitTransform;
	private Vector3 _collisionPositionOffset;
	private Quaternion _rotationWhenHit;
	private Quaternion _collisionRotationWhenHit;

	private void OnEnable()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody2D>();
		_collider = GetComponent<PolygonCollider2D>();
		_rb.velocity = transform.right * velocity;
	}

	private void FixedUpdate()
	{
		if (_collisionHitTransform == null) return;
		Quaternion rotation = _collisionHitTransform.rotation * Quaternion.Inverse(_collisionRotationWhenHit);
		_transform.position = _collisionHitTransform.position + rotation*_collisionPositionOffset;
		_transform.rotation = rotation * _rotationWhenHit;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		_rb.velocity = Vector2.zero;
		_collider.enabled = false;
		_collisionHitTransform = col.transform;
		_collisionPositionOffset = _transform.position - _collisionHitTransform.position;
		_rotationWhenHit = _transform.rotation;
		_collisionRotationWhenHit = _collisionHitTransform.rotation;
		Destroy(gameObject, lifetimePostCollision);
		Player player = col.gameObject.GetComponent<Player>();
		if (player != null)
		{
			player.GetComponent<HealthBase>().HealthLevel -= damage;
		}
	}
}
