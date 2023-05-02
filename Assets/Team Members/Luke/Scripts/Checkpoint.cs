using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
	private SaveData _saveData = null;
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (GameManager.Instance.saveData == _saveData) return;
		
		SaveGame();
	}

	public void SaveGame()
	{
		Player _player = GameManager.Instance.cameraTracker.playerTransform.GetComponent<Player>();
		GameManager gameManager = GameManager.Instance;
		_saveData = gameManager.saveData;
		gameManager.saveData.playerPosition = transform.position;
		gameManager.saveData.sceneName = gameObject.scene.name;
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
