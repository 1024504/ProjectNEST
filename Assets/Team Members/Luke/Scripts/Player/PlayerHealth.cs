using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthBase
{
	public float health;

	
	public void Awake()
	{
		health = maxHealth;
	}

	protected override void Die()
	{
		//Die Logic
	}
}
