using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	public string sceneName;
	public Vector3 playerPosition;
	public bool hasShotgun;
	public bool hasSniper;
	public bool canDoubleJump;
	public bool canGrapple;
	public int totalMedkits;
	public List<ObjectiveStringPair> objectives;
	public List<MyCollectibleBoolPair> collectibles;
	public SettingsData SettingsData;
	
	public SaveData(string sceneNameString = "", Vector3 playerPositionVector3 = new (), bool hasShotgunBool = false,
		bool hasSniperBool = false, bool canDoubleJumpBool = false, bool canGrappleBool = false, int totalMedkitsInt = 0,
		List<ObjectiveStringPair> objectivesList = null, List<MyCollectibleBoolPair> collectiblesList = null, SettingsData settingsDataData = new ())
	{
		sceneName = sceneNameString;
		playerPosition = playerPositionVector3;
		hasShotgun = hasShotgunBool;
		hasSniper = hasSniperBool;
		canDoubleJump = canDoubleJumpBool;
		canGrapple = canGrappleBool;
		totalMedkits = totalMedkitsInt;
		objectives = objectivesList;
		collectibles = collectiblesList;
		SettingsData = settingsDataData;
	}
}

[System.Serializable]
public struct SettingsData
{
	// Update this when more settings are added
	public SettingsData(bool toggleSprint, float leftStickDeadzone, bool toggleSubtitles, bool toggleHUD,
		float masterVolume, float musicVolume, float sfxVolume, Resolution resolution, bool fullscreen, int quality)
	{
		ToggleSprint = toggleSprint;
		LeftStickDeadzone = leftStickDeadzone;
		ToggleSubtitles = toggleSubtitles;
		ToggleHUD = toggleHUD;
		MasterVolume = masterVolume;
		MusicVolume = musicVolume;
		SFXVolume = sfxVolume;
		Resolution = resolution;
		Fullscreen = fullscreen;
		Quality = quality;
	}
	
	// Controls
	public bool ToggleSprint;
	public float LeftStickDeadzone;
	// public float RightStickDeadzone;

	// Gameplay
	public bool ToggleSubtitles;
	public bool ToggleHUD;
	// public bool toggleTutorial;
	
	// Audio
	public float MasterVolume;
	public float MusicVolume;
	public float SFXVolume;
	
	// Screen
	public Resolution Resolution;
	public bool Fullscreen;
	public int Quality;
}
