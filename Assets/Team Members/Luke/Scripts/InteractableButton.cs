using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : InteractableObject
{
	[SerializeField] GameObject buttonUI;
	
	protected override void Interact()
	{
		OnInteract?.Invoke();
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

	
}