using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;


public class TriggerCutScene : MonoBehaviour
{
    public PlayableDirector timeLineDirector;
    public bool isTimeStop;

    public void OnEnable()
    {
        timeLineDirector.played += DisableControls;
        timeLineDirector.stopped += EnableControls;
    }
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            timeLineDirector.Play();
        }
    }
    
    public void DisableControls(PlayableDirector obj)
    {
        if(isTimeStop) GameManager.Instance.playerController.Controls.Disable();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
	
    public void EnableControls(PlayableDirector obj)
    {
        if(isTimeStop) GameManager.Instance.playerController.Controls.Enable();
    }
    
    public void OnDisable()
    {
        timeLineDirector.played -= DisableControls;
        timeLineDirector.stopped -= EnableControls;
    }
}
