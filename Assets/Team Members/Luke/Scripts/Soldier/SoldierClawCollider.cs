using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoldierClawCollider : MonoBehaviour
{
	AlveriumSoldier _agent;
	public GameObject bloodParticle;
	public EventReference impactSFX;
	
	private void OnEnable()
	{
		_agent = GetComponentInParent<AlveriumSoldier>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		Player player = col.gameObject.GetComponent<Player>();
		if (player != null)
		{
			Instantiate(bloodParticle, player.transform.position, Quaternion.identity);
			RuntimeManager.PlayOneShot(impactSFX);
			player.GetComponent<HealthBase>().HealthLevel -= _agent.attackDamage;
		}
	}
}
