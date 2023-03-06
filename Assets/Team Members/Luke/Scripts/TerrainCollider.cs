using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollider : MonoBehaviour
{
	protected Transform _t;

	public bool isGrounded = true;
	public Vector2 normal;

	public List<Collider2D> _groundColliders = new ();
	
	protected virtual void OnEnable()
	{
		_t = transform;
	}

	protected void Update()
	{
		if (_groundColliders.Count == 0) return;
		foreach (Collider2D collider in _groundColliders)
		{
			if (collider != null) continue;
			_groundColliders.Remove(collider);
			if (_groundColliders.Count == 0) isGrounded = false;
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (!_groundColliders.Contains(other)) _groundColliders.Add(other);
		isGrounded = true;
	}

	protected virtual void OnTriggerExit2D(Collider2D other)
	{
		if (_groundColliders.Contains(other)) _groundColliders.Remove(other);
	}
}
