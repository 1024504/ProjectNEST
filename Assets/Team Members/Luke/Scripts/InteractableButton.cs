using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class InteractableButton : InteractableObject
{
	[SerializeField] GameObject buttonUI;
	public PlayableDirector timeLineDirector;
	public EventReference interactSFX;
	public EventReference dialogueAudio;

	public void OnEnable()
	{
		timeLineDirector.played += PlayCutScene;
		timeLineDirector.stopped += StopCutScene;
	}
	
	protected override void Interact()
	{
		OnInteract?.Invoke();
		RuntimeManager.PlayOneShot(interactSFX);
		RuntimeManager.PlayOneShot(dialogueAudio);
		timeLineDirector.Play();
		if (objective != GameManager.Objectives.None)
		{
			GameManager.Instance.UpdateObjective(objective);
		}
		if( singleUse )
        {
			buttonUI.gameObject.SetActive(false);
			this.GetComponent<InteractableButton>().enabled = false;
		}
	}

	public void PlayCutScene(PlayableDirector obj)
	{
		GameManager.Instance.playerController.Controls.Disable();
		Debug.Log("Start");
	}
	
	public void StopCutScene(PlayableDirector obj)
	{
		GameManager.Instance.playerController.Controls.Enable();
		Debug.Log("Stop");
	}
}
