using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoldierClawCollider : MonoBehaviour
{
	AlveriumSoldier _agent;
	
	private void OnEnable()
	{
		_agent = GetComponentInParent<AlveriumSoldier>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		Player player = col.gameObject.GetComponent<Player>();
		if (player != null)
		{
			
			player.GetComponent<HealthBase>().HealthLevel -= _agent.attackDamage;
		}
	}
}
