using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBase
{
	public Transform respawnPoint;
	protected override void Die()
	{
		GameObject go = gameObject.GetComponentInParent<Player>().gameObject;
		go.transform.position = respawnPoint.position;
		HealthLevel = maxHealth;
		GameManager.Instance.survivalTimer = 0f;
		GameManager.Instance.killCount = 0;
	}
}
