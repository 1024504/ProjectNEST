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
		GameManager gameManager = GameManager.Instance;
		if (gameManager.saveData.playerPosition == transform.position) return;
		gameManager.saveData.playerPosition = transform.position;
		gameManager.saveData.sceneName = SceneManager.GetActiveScene().name;
		gameManager.saveData.hasShotgun = _player.hasShotgun;
		gameManager.saveData.hasSniper = _player.hasSniper;
		gameManager.saveData.canDoubleJump = _player.doubleJumpEnabled;
		gameManager.saveData.canGrapple = _player.grappleEnabled;
		gameManager.saveData.totalMedkits = _player.medkitCount;
		gameManager.saveData.currentMusicTrack = gameManager.GetComponentInChildren<MusicManagerScript>().TrackSelector;
		gameManager.SaveGame();
	}

	public void DialogueSave()
	{
		SaveGame();
	}
}
