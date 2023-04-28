using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
	private Player _player;
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		_player = col.GetComponent<Player>();
		if (_player == null) return;

		SaveGame();
	}

	private void SaveGame()
	{
		if (GameManager.Instance.saveData.playerPosition == transform.position) return;
		GameManager.Instance.saveData.playerPosition = transform.position;
		GameManager.Instance.saveData.sceneName = SceneManager.GetActiveScene().name;
		GameManager.Instance.saveData.hasShotgun = _player.hasShotgun;
		GameManager.Instance.saveData.hasSniper = _player.hasSniper;
		GameManager.Instance.saveData.canDoubleJump = _player.doubleJumpEnabled;
		GameManager.Instance.saveData.canGrapple = _player.grappleEnabled;
		GameManager.Instance.saveData.totalMedkits = _player.medkitCount;
		GameManager.Instance.SaveGame();
	}

	public void DialogueSave()
	{
		SaveGame();
	}
}
