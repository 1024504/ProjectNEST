using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
	[HideInInspector] public HealthBase health;

	protected virtual void OnEnable()
	{
		health = GetComponentInParent<HealthBase>();
	}
}
