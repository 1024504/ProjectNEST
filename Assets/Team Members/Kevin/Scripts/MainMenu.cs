using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartNewGameButton()
    {
        Debug.Log("New Game Started");
    }

    public void LoadSavedGame()
    {
        Debug.Log("Load Saved Game");
    }
    public void ExitGameButton()
    {
        Debug.Log("Game Exited");
        Application.Quit();
    }
}
