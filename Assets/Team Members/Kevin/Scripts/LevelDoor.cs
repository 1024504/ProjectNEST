using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelDoor : InteractableObject
{
    public LevelDoor connectingDoor;
    //public CameraTracker _cameraTracker;
    [HideInInspector]
    public Player player;
    
    public FMODUnity.EventReference doorSFX;
    
    protected override void Interact()
    {
	    base.Interact();
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
	    /*GameManager.Instance.DisableInput();
	    _cameraTracker = GameManager.Instance.cameraTracker;
	    _cameraTracker.cameraFader.OnFadeOutComplete += LoadStage1;
	    _cameraTracker.cameraFader.FadeOut();*/
	    player.transform.position = connectingDoor.transform.position;
	    connectingDoor.player = player;
    }

    /*private void LoadStage1()
    {
	    _cameraTracker.cameraFader.OnFadeOutComplete -= LoadStage1;
	    _cameraTracker.cameraFader.OnFadeOutComplete += LoadStage2;
	    _cameraTracker.cameraFader.FadeIn();
    }
    
    private void LoadStage2()
    {
	    _cameraTracker.cameraFader.OnFadeInComplete -= LoadStage2;
	    GameManager.Instance.EnableInput();
    }*/
    
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
