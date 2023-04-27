using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectiveStringPair
{
	public GameManager.Objectives objective = GameManager.Objectives.None;
	public string uiText = "";
	public bool isCompleted = false;
	public bool isHidden = false;
}

[Serializable]
public class MyCollectibleBoolPair
{
	public MyCollectible collectible = 0;
	public bool isCollected = false;
}

[Serializable]
public class MyCollectibleGameObjectPair
{
	public MyCollectible collectible = 0;
	public GameObject gameObject = null;
}
