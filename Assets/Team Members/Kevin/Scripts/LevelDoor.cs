using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelDoor : InteractableObject
{
    public LevelDoor connectingDoor;

    [HideInInspector]
    public Player player;
    
    public FMODUnity.EventReference doorSFX;

    protected override void Interact()
    {
	    if (player == null)
	    {
		    Debug.Log("Player is null");
		    return;
	    }
	    
	    if (connectingDoor == null)
	    {
		    Debug.Log("Connecting door is null");
		    return;
	    }
	    
	    FMODUnity.RuntimeManager.PlayOneShot(doorSFX);
	    
	    player.transform.position = connectingDoor.transform.position;
	    connectingDoor.player = player;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
	    base.OnTriggerEnter2D(col);
	    player = col.GetComponent<Player>();
    }
    
    protected override void OnTriggerExit2D(Collider2D col)
	{
	    base.OnTriggerExit2D(col);
	    player = null;
	}
}
