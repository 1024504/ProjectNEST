using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    bool skipActive = false;
    [SerializeField] GameObject skipPrompt;
    CutsceneSceneChanger sceneChanger;

    private void Start()
    {
        sceneChanger = this.GetComponent<CutsceneSceneChanger>(); ;
        skipActive = false;
    }

    public void SkipButtonPressed() { 
        if( !skipActive )
        {
            ActivateSkip();
        } else
        {
            SkipCredits();
        }
    }

    public void ActivateSkip()
    {
        skipActive = true;
        skipPrompt.SetActive(true);
    }


    void SkipCredits()
    {
        sceneChanger.LoadMainMenu();
    }
}
