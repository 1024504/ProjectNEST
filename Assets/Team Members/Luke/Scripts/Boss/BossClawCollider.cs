using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClawCollider : MonoBehaviour
{
	AlveriumBoss _agent;
	
	private void OnEnable()
	{
		_agent = GetComponentInParent<AlveriumBoss>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (!_agent.canDealDamage) return;
		Player player = col.gameObject.GetComponent<Player>();
		if (player == null) return;
		
		player.GetComponent<HealthBase>().HealthLevel -= _agent.meleeAttackDamage;
		_agent.canDealDamage = false;
	}
}
