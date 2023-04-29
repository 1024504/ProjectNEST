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
	public int currentMusicTrack;
	public SettingsData SettingsData;

	public List<ObjectiveStringPair> defaultObjectives = new List<ObjectiveStringPair>()
	{
		new (GameManager.Objectives.EscapeHangar, "Open the hangar door", false, false),
		new (GameManager.Objectives.TurnOnGenerator, "Turn on power", false, true),
		new (GameManager.Objectives.ExploreLab, "Investigate the lab", false, true),
		new (GameManager.Objectives.EnterPlaza, "Enter the plaza", false, true),
		new (GameManager.Objectives.FindHawk, "Find Hawk", false, true),
		new (GameManager.Objectives.FindRaven, "Find Raven", false, true),
		new (GameManager.Objectives.FindEagle, "Find Eagle", false, true),
		new (GameManager.Objectives.DefeatBoss, "Defeat the Warrior", false, true)
	};
	
	public SaveData(string sceneNameString = "", Vector3 playerPositionVector3 = new (), bool hasShotgunBool = false,
		bool hasSniperBool = false, bool canDoubleJumpBool = false, bool canGrappleBool = false, int totalMedkitsInt = 0,
		List<ObjectiveStringPair> objectivesList = null, List<MyCollectibleBoolPair> collectiblesList = null, int currentTrack = 0, SettingsData settingsDataData = new ())
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
		currentMusicTrack = currentTrack;
		SettingsData = settingsDataData;
	}
}

[System.Serializable]
public struct SettingsData
{
	// Update this when more settings are added
	public SettingsData(string controlsOverrides, bool toggleSprint, float leftStickDeadzone, bool toggleSubtitles, bool toggleHUD,
		float masterVolume, float musicVolume, float sfxVolume, Resolution resolution, bool fullscreen, int quality)
	{
		ControlsOverrides = controlsOverrides;
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
	public string ControlsOverrides;
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
