using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetection : MonoBehaviour
{
	protected Transform _t;

	public bool isGrounded = false;
	public Vector2 mainNormal;

	protected virtual void OnEnable()
	{
		_t = transform;
	}
}
