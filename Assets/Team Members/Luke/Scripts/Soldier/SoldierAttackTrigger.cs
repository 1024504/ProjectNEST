using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierAttackTrigger : MonoBehaviour
{
	private AlveriumSoldier _agent;
	
	private void OnEnable()
	{
		_agent = GetComponentInParent<AlveriumSoldier>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player == null) return;
		_agent.targetWithinRange = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player == null) return;
		_agent.targetWithinRange = false;
	}
}
