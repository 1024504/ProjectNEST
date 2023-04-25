using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackDialogueGameObject : MonoBehaviour
{
    public GameObject nextDialogueObject;
    
    public void NextDialogueActivate()
    {
        StartCoroutine(Activate());
        
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(4f);
        nextDialogueObject.SetActive(true);
    }
}
