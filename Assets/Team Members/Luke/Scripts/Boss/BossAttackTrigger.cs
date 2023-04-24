using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{
	private AlveriumBoss _agent;
	private Player _player;

	private void OnEnable()
	{
		_agent = GetComponentInParent<AlveriumBoss>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player == null) return;
		_agent.targetInMeleeRange = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player == null) return;
		_agent.targetInMeleeRange = false;
	}
}
