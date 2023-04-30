using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectiveStringPair
{
	public ObjectiveStringPair(GameManager.Objectives newObjective, string uiTextString = "", bool isCompletedBool = false, bool isHiddenBool = false)
	{
		objective = newObjective;
		uiText = uiTextString;
		isCompleted = isCompletedBool;
		isHidden = isHiddenBool;
	}
	
	public GameManager.Objectives objective = GameManager.Objectives.None;
	public string uiText = "";
	public bool isCompleted = false;
	public bool isHidden = false;
}

[Serializable]
public class MyCollectibleBoolPair
{
	public MyCollectibleBoolPair(MyCollectible newCollectible, bool isCollectedBool = false)
	{
		collectible = newCollectible;
		isCollected = isCollectedBool;
	}
	
	public MyCollectible collectible = 0;
	public bool isCollected = false;
}

[Serializable]
public class MyCollectibleGameObjectPair
{
	public MyCollectible collectible = 0;
	public GameObject gameObject = null;
}
