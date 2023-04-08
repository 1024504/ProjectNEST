using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadTrigger : MonoBehaviour
{
	public string sceneToLoad;
	public bool doNotUnload = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player == null) return;

		if (sceneToLoad == null)
		{
			Debug.Log("Scene to load is null");
			return;
		}
		
		if (!SceneManager.GetSceneByName(sceneToLoad).isLoaded) LevelManager.Instance.LoadScene(sceneToLoad);
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player == null) return;
		
		if (sceneToLoad == null)
		{
			Debug.Log("Scene to unload is null");
			return;
		}

		if (SceneManager.GetSceneByName(sceneToLoad).isLoaded && !doNotUnload) LevelManager.Instance.UnloadScene(sceneToLoad);
	}
}
