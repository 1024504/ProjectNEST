using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
	private Transform _t;

	public bool isGrounded = true;
	public Vector2 normal;

	private readonly List<Collider2D> _groundColliders = new ();
	
	private void OnEnable()
	{
		_t = transform;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!_groundColliders.Contains(other)) _groundColliders.Add(other);
		isGrounded = true;
	}
	
	private void FixedUpdate()
	{
		Vector3 position = _t.position;

		if (_groundColliders.Count > 0)
		{
			float closestDistance = Vector2.Distance(position, _groundColliders[0].ClosestPoint(position));
			int closestIndex = 0;

			if (_groundColliders.Count > 1)
			{
				for (int i = 1; i < _groundColliders.Count-1; i++)
				{
					if (Vector2.Distance(position, _groundColliders[i].ClosestPoint(position)) < closestDistance)
						closestIndex = i;
				}
			}
			normal = Physics2D.Linecast(position, _groundColliders[closestIndex].ClosestPoint(position)).normal;
			Debug.DrawRay(_groundColliders[closestIndex].ClosestPoint(position),normal, Color.green);
		}
		else
		{
			isGrounded = false;
			normal = Vector2.up;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (_groundColliders.Contains(other)) _groundColliders.Remove(other);
	}
}
