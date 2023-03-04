using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenuManager : MonoBehaviour
{
    public string testFormLink;
    public List<string> scenes;
    public void CombatSceneButton()
    {
        SceneManager.LoadScene(scenes[0]);
    }

    public void MovementTestOneSceneButton()
    {
        SceneManager.LoadScene(scenes[1]);
    }

    public void MovementTestTwoSceneButton()
    {
        SceneManager.LoadScene(scenes[2]);
    }

    public void FeedbackFormButton()
    {
        Application.OpenURL(testFormLink);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
    
}
