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
	public bool isTimeStop;
	
	public void OnEnable()
	{
		if (timeLineDirector == null) return;
		timeLineDirector.played += DisableControls;
		timeLineDirector.stopped += EnableControls;
	}
	
	protected override void Interact()
	{
		base.Interact();
		OnInteract?.Invoke();
		if (timeLineDirector != null) timeLineDirector.Play();
		if( singleUse )
        {
			buttonUI.gameObject.SetActive(false);
			GetComponent<Collider2D>().enabled = false;
			GetComponent<InteractableButton>().enabled = false;
        }
	}

	public void DisableControls(PlayableDirector obj)
	{
		if(isTimeStop) GameManager.Instance.playerController.Controls.Disable();
	}
	
	public void EnableControls(PlayableDirector obj)
	{
		if(isTimeStop) GameManager.Instance.playerController.Controls.Enable();
	}
	
	/*public void OnDisable()
	{
		timeLineDirector.played -= DisableControls;
		timeLineDirector.stopped -= EnableControls;
	}*/
}
