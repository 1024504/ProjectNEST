using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndIntro : MonoBehaviour
{

    public void IntroFinish()
    {
        SceneManager.LoadScene("TestDemoMainMenu");
    }
}
