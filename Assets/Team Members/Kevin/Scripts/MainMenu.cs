using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string testFormLink;
    public Animator mmAnimator;

    public void Awake()
    {
        mmAnimator.CrossFade("Opening", 0, 0);
    }
    public void StartNewGameButton()
    {
        SceneManager.LoadScene("GreyboxLevel1Tutorial");
    }

    public void LoadSavedGame()
    {
        Debug.Log("Load Saved Game");
    }

    public void FeedbackFormButton()
    {
        Application.OpenURL(testFormLink);
    }
    public void ExitGameButton()
    {
        Debug.Log("Game Exited");
        Application.Quit();
    }

    public void OptionsAnimIN()
    {
        mmAnimator.CrossFade("Options", 0, 0);
    }

    public void OptionsAnimOut()
    {
        mmAnimator.CrossFade("Options 0", 0, 0);
    }
}
