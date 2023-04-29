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
	public GameObject doorObject;
	public PlayableDirector timeLineDirector;
	public bool isTimeStop;

	public GameManager.Objectives linkedObjective;
	
	public void OnEnable()
	{
		GameManager.Instance.OnFinishLoading += CheckObjective;
		LevelManager.Instance.OnSceneLoaded += CheckObjective;
		
		if (timeLineDirector == null) return;
		timeLineDirector.played += DisableControls;
		timeLineDirector.stopped += EnableControls;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		GameManager.Instance.OnFinishLoading -= CheckObjective;
		LevelManager.Instance.OnSceneLoaded -= CheckObjective;
	}

	private void CheckObjective()
	{
		foreach (ObjectiveStringPair objective in GameManager.Instance.saveData.objectives)
		{
			if (gameObject == null) return;
			if (objective.objective != linkedObjective) continue;
			if (objective.isCompleted)
			{
				Debug.Log("Gotteeem");
				if(doorObject!=null) doorObject.SetActive(false);
				if(buttonUI!= null) buttonUI.SetActive(false);
				if(gameObject != null) gameObject.SetActive(false);
			}
			break;
		}
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
