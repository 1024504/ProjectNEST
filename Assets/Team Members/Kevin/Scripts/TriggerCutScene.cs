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
    
    public GameManager.Objectives linkedObjective;
    
    private bool _isTriggered;

    public void OnEnable()
    {
	    GameManager.Instance.OnFinishLoading += CheckObjective;
	    LevelManager.Instance.OnSceneLoaded += CheckObjective;
	    
        timeLineDirector.played += DisableControls;
        timeLineDirector.stopped += EnableControls;
    }
    
    public void OnTriggerEnter2D(Collider2D col)
    {
	    if (_isTriggered) return;
	    Player player = col.GetComponent<Player>();
	    if (player != null)
        {
            timeLineDirector.Play();
            _isTriggered = true;
        }
    }

    public void CheckObjective()
    {
	    foreach (ObjectiveStringPair objective in GameManager.Instance.saveData.objectives)
	    {
		    if (gameObject == null) return;
		    if (objective.objective != linkedObjective) continue;
		    if (objective.isCompleted)
		    {
			    if(gameObject != null) gameObject.SetActive(false);
		    }
		    break;
	    }
    }
    
    public void DisableControls(PlayableDirector obj)
    {
        if(isTimeStop) GameManager.Instance.playerController.Controls.Disable();
        if(gameObject.GetComponent<BoxCollider2D>() != null) gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
	
    public void EnableControls(PlayableDirector obj)
    {
        if(isTimeStop) GameManager.Instance.playerController.Controls.Enable();
    }
    
    public void OnDisable()
    {
	    GameManager.Instance.OnFinishLoading -= CheckObjective;
	    LevelManager.Instance.OnSceneLoaded -= CheckObjective;
	    
        timeLineDirector.played -= DisableControls;
        timeLineDirector.stopped -= EnableControls;
    }
}
