using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerREF : MonoBehaviour
{
	private GameObject musicManager;

	private void Start()
	{
		musicManager = GameObject.Find("MusicManager");
	}
}
