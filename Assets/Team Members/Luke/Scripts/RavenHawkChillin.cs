using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RavenHawkChillin : MonoBehaviour
{
	public GameObject ravenHawk;
	public GameObject door;
	
    private void OnEnable()
    {
	    GameManager.Instance.OnFinishLoading += CheckObjective;
	    LevelManager.Instance.OnSceneLoaded += CheckObjective;
    }

    private void OnDisable()
    {
	    GameManager.Instance.OnFinishLoading -= CheckObjective;
	    LevelManager.Instance.OnSceneLoaded -= CheckObjective;
    }

    private void CheckObjective()
    {
	    if (!GameManager.Instance.saveData.objectives[1].isCompleted) return;
	    door.SetActive(false);
	    ravenHawk.SetActive(false);
    }
}
