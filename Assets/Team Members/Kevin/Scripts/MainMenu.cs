using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string testFormLink;
    public void StartNewGameButton()
    {
        SceneManager.LoadScene("MainScene");
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
}
