using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthBase
{

	public delegate void DeathMenu();
	
	protected override void Die()
	{
		OnDeath?.Invoke();
		healthLevel = 0;
	}
}
