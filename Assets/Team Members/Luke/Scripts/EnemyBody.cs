using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
	public Health health;

	private void OnEnable()
	{
		health = GetComponentInParent<Health>();
	}
}
