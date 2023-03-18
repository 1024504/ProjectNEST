using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthBase
{
	protected override void Die()
	{
		healthLevel = 0;
		Debug.Log("Died");
	}
}
