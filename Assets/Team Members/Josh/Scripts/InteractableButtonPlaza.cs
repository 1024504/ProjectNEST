using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButtonPlaza : InteractableObject
{
    [SerializeField] GameObject popupPrompt;
	[SerializeField] GameObject buttonUI;

	protected override void Interact()
	{
		if( singleUse )
		{
			buttonUI.gameObject.SetActive(false);
			GetComponent<Collider2D>().enabled = false;
			GetComponent<InteractableButtonPlaza>().enabled = false;
			popupPrompt.SetActive(true);
		}
	}
}
