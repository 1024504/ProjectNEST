using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicManagerScript : MonoBehaviour
{
	public FMODUnity.EventReference fmodEvent;
	private FMOD.Studio.EventInstance music;
	
	private int trackSelector;

	public int TrackSelector
	{
		get => trackSelector;
		
		set
		{
			if (value == trackSelector) return;
			trackSelector = value;
			UpdateParameter();
		}
	}

	public void Awake()
	{
		music = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
		music.start();
	}

	private void UpdateParameter()
	{
		music.setParameterByName("TrackSelection", trackSelector);
	}
}
