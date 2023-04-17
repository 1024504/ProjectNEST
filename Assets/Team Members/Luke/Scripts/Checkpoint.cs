using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public FMODUnity.EventReference savedTriggered;
	private void OnTriggerEnter2D(Collider2D col)
	{
		GameManager.Instance.SaveCheckpoint(this);
		FMODUnity.RuntimeManager.PlayOneShot(savedTriggered);
		GameManager.Instance.uiManager.saveIconUI.GetComponent<Animator>().SetTrigger("TriggerSaveANIM");
		Debug.Log("Saved");
	}
}
