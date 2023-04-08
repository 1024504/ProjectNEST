using System.Collections;
using System.Collections.Generic;
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
	
	public SaveData(string sceneNameString, Vector3 playerPositionVector3, float playerHealthFloat, bool hasShotgunBool, bool hasSniperBool, bool canDoubleJumpBool, bool canGrappleBool, int totalMedkitsInt, List<ObjectiveStringPair> objectivesList)
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
	}
}
