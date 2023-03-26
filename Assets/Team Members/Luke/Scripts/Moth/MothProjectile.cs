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
	private Quaternion _collisionRotationOffset;

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
		_transform.position = _collisionHitTransform.position + _collisionPositionOffset;
		_transform.rotation = _collisionHitTransform.rotation * _collisionRotationOffset;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log(col.transform.name);
		_rb.velocity = Vector2.zero;
		_collider.enabled = false;
		_collisionHitTransform = col.transform;
		_collisionPositionOffset = _transform.position - _collisionHitTransform.position;
		_collisionRotationOffset = _transform.rotation * Quaternion.Inverse(_collisionHitTransform.rotation);
		Destroy(gameObject, lifetimePostCollision);
		Player player = col.gameObject.GetComponent<Player>();
		if (player != null)
		{
			player.GetComponent<HealthBase>().HealthLevel -= damage;
		}
	}
}
