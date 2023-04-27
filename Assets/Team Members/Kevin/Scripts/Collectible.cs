using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Collectible : MonoBehaviour
{
	public MyCollectible MyCollectible;
}

[Serializable]
public enum MyCollectible
{
	Plaza1,
	Plaza2,
	Plaza3,
	ResArea1,
	ResArea2,
	ResArea3,
	Bio1,
	Bio2,
	Bio3
}