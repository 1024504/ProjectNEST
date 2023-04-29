using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneSceneChanger : MonoBehaviour
{
	private GameManager _gm;

	private void Start()
	{
		_gm = GameManager.Instance;
	}

	public void LoadHangar()
	{
		SceneManager.sceneLoaded += _gm.SetupAfterLevelLoad;
		SceneManager.LoadScene("Level1_Hangar&Lab");
	}

	public void LoadCredits()
	{
		SceneManager.LoadScene("Credits");
	}
	
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
