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
}
