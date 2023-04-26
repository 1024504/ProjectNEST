using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneBorder_Script : MonoBehaviour
{
	public GameObject cutsceneBorderPrefab;

	public Animator borderANIMATOR;

	public bool cutsceneFinished;

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
	}

	public void DisableBorder()
	{
		if( cutsceneFinished == true )
		{
			cutsceneBorderPrefab.SetActive(false);
			cutsceneFinished = false;
		}
	}

	public void CutsceneBoolFalse()
	{
		cutsceneFinished = true;
	}
}
