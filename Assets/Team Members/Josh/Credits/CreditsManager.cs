using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    bool skipActive=false;
    private GameObject skipPrompt;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
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
        gameManager.QuitGame1();
    }
}
