using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneBorder_Script : MonoBehaviour
{
	public GameObject cutsceneBorderPrefab;

	public Animator borderANIMATOR;

	public bool cutsceneFinished;

	//HUD remove stuff
	private GameManager gameManagerRef;
    private void Start()
    {
		gameManagerRef = GameManager.Instance;
	}

    public void Update()
	{
		CutsceneFinished();
	}

	public void CutsceneFinished()
	{
		if( cutsceneFinished == true )
		{
			borderANIMATOR.CrossFade("Border_OUT", 0, 0);
		}
	}

	public void borderEnable()
	{
		cutsceneBorderPrefab.SetActive(true);

		//HUD remove stuff
		if( gameManagerRef.saveData.SettingsData.ToggleHUD )
        {
			gameManagerRef.uiManager.hUDGameObject.SetActive(false);
		}
	}

	public void DisableBorder()
	{
		if( cutsceneFinished == true )
		{
			cutsceneBorderPrefab.SetActive(false);
			cutsceneFinished = false;

			//HUD remove stuff
			if( gameManagerRef.saveData.SettingsData.ToggleHUD )
			{
				gameManagerRef.uiManager.hUDGameObject.SetActive(true);
			}
		}
	}

	public void CutsceneBoolFalse()
	{
		cutsceneFinished = true;
	}
}
