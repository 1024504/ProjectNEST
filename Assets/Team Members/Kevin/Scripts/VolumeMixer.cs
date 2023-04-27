using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class VolumeMixer : MonoBehaviour
{
	private FMOD.Studio.Bus masterBus;
	private FMOD.Studio.Bus musicBus;
	private FMOD.Studio.Bus sfxBus;

	public float masterVolume = 1;
	public float sfxVolume = 1;
	public float musicVolume = 1;

	private FMOD.Studio.EventInstance sfxVolumeTester;

	private void Awake()
	{
		masterBus = FMODUnity.RuntimeManager.GetBus("bus:/Master");
		musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
		sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");

		sfxVolumeTester = FMODUnity.RuntimeManager.CreateInstance("event:/Characters/Gun SFX/RifleSFX/RifleShots");
	}

	public void Update()
	{
		masterBus.setVolume(masterVolume);
		musicBus.setVolume(musicVolume);
		sfxBus.setVolume(sfxVolume);
	}

	public void MasterVolumeLevel(float newMasterVolume)
	{
		masterVolume = newMasterVolume;
	}

	public void MusicVolume(float newMusicVolume)
	{
		musicVolume = newMusicVolume;
	}

	public void SFXVolume(float newSFXVolume)
	{
		sfxVolume = newSFXVolume;

		FMOD.Studio.PLAYBACK_STATE PbState;
		sfxVolumeTester.getPlaybackState(out PbState);
		if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
		{
			sfxVolumeTester.start();
		}
	}
}
