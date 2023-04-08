using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col)
	{
		GameManager.Instance.SaveCheckpoint(this);
		Debug.Log("Saved");
	}
}
