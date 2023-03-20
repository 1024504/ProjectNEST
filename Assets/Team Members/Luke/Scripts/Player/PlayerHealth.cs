using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthBase
{

	public delegate void DeathMenu();

	public event DeathMenu OnDeath;
	protected override void Die()
	{
		healthLevel = 0;
		OnDeath?.Invoke();
	}
}
