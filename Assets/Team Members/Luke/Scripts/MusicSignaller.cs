using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicSignaller : MonoBehaviour
{
	private MusicManagerScript _musicManagerScript;
	
	private void OnEnable()
	{
		_musicManagerScript = GameManager.Instance.GetComponentInChildren<MusicManagerScript>();
	}

	public void SetTrack(int track)
	{
		_musicManagerScript.TrackSelector = track;
	}
}
