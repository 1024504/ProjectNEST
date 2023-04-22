using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col)
	{
		Player player = col.GetComponent<Player>();
		if (player == null) return;
		if (GameManager.Instance.saveData.playerPosition == transform.position) return;
		GameManager.Instance.SaveCheckpoint(this);
	}
}
