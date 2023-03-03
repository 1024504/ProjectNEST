using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenuManager : MonoBehaviour
{
    public string testFormLink;
    public void CombatSceneButton()
    {
        //load combat test scene
    }

    public void MovementTestOneSceneButton()
    {
        SceneManager.LoadScene("Playtest_1_BUILD/Platforming Test A1");
    }

    public void MovementTestTwoSceneButton()
    {
        SceneManager.LoadScene("Playtest_1_BUILD/Platforming Test A2");
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
