using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	public string sceneName;
	public Vector3 playerPosition;
	public float playerHealth;
	public bool hasShotgun;
	public bool hasSniper;
	public bool canDoubleJump;
	public bool canGrapple;
	public int totalMedkits;
	public List<ObjectiveStringPair> objectives;
	public SettingsData SettingsData;
	
	public SaveData(string sceneNameString = "", Vector3 playerPositionVector3 = new (), float playerHealthFloat = 0f,
		bool hasShotgunBool = false, bool hasSniperBool = false, bool canDoubleJumpBool = false, bool canGrappleBool = false,
		int totalMedkitsInt = 0, List<ObjectiveStringPair> objectivesList = null, SettingsData settingsDataData = new ())
	{
		sceneName = sceneNameString;
		playerPosition = playerPositionVector3;
		playerHealth = playerHealthFloat;
		hasShotgun = hasShotgunBool;
		hasSniper = hasSniperBool;
		canDoubleJump = canDoubleJumpBool;
		canGrapple = canGrappleBool;
		totalMedkits = totalMedkitsInt;
		objectives = objectivesList;
		SettingsData = settingsDataData;
	}
}

[System.Serializable]
public struct SettingsData
{
	// Update this when more settings are added
	public SettingsData(bool toggleSprint, float leftStickDeadzone, Resolution resolution, bool fullscreen, int quality)
	{
		ToggleSprint = toggleSprint;
		LeftStickDeadzone = leftStickDeadzone;
		Resolution = resolution;
		Fullscreen = fullscreen;
		Quality = quality;
	}
	
	// Controls
	public bool ToggleSprint;
	public float LeftStickDeadzone;
	// public float RightStickDeadzone;
	
	// Gameplay
	
	
	// Audio
	
	
	// Screen
	public Resolution Resolution;
	public bool Fullscreen;
	public int Quality;
}
