using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    public InteractableObject trigger;

    private void OnEnable()
    {
	    if (trigger != null)
	    {
		    trigger.OnInteract += OpenDoor;
	    }
    }
    
    private void OpenDoor()
    {
	    gameObject.SetActive(false);
	}
}
