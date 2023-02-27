using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenuManager : MonoBehaviour
{
    public string testFormLink;
    public void CombatSceneButton()
    {
        //load combat test scene
    }

    public void MovementTestOneSceneButton()
    {
        //load movement test 1 scene
    }

    public void MovementTestTwoSceneButton()
    {
        //load movement test 2 scene
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
