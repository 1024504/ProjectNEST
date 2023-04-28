using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMixer : MonoBehaviour
{
	[SerializeField] private Settings settings;

	// private FMOD.Studio.EventInstance sfxVolumeTester;

	private void Awake()
	{
		foreach (Slider slider in GetComponentsInChildren<Slider>())
		{
			if (gameObject.name == "MasterVolumeSlider") slider.value = settings.masterVolume;
			else if (gameObject.name == "MusicVolumeSlider") slider.value = settings.musicVolume;
			else if (gameObject.name == "SFXVolumeSlider") slider.value = settings.sfxVolume;
		}
		
		// sfxVolumeTester = FMODUnity.RuntimeManager.CreateInstance("event:/Characters/Gun SFX/RifleSFX/RifleShots");
	}

	public void MasterVolumeLevel(float newMasterVolume)
	{
		settings.masterVolume = newMasterVolume;
	}

	public void MusicVolume(float newMusicVolume)
	{
		settings.musicVolume = newMusicVolume;
	}

	public void SFXVolume(float newSFXVolume)
	{
		settings.sfxVolume = newSFXVolume;

		// FMOD.Studio.PLAYBACK_STATE PbState;
		// sfxVolumeTester.getPlaybackState(out PbState);
		// if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
		// {
		// 	sfxVolumeTester.start();
		// }
	}
}
