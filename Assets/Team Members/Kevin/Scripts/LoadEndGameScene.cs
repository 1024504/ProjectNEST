using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEndGameScene : MonoBehaviour
{
    public void LoadEndScene()
    {
        GameManager.Instance.FinishGame1();
    }
    
}
