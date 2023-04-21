using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventBase : MonoBehaviour
{
    public FMODUnity.EventReference eventToTrigger;
    public bool isSingleUse;
    public float pauseTimer;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && isSingleUse)
        {
            //GameManager.Instance.uiManager.saveIconUI.GetComponent<Animator>().SetTrigger("TriggerSaveANIM");
            RunDialogue();
        }
    }

    public virtual void RunDialogue()
    {
        isSingleUse = false;
        FMODUnity.RuntimeManager.PlayOneShot(eventToTrigger);
        GameManager.Instance.uiManager.visualiserHUD.SetActive(true);
        GameManager.Instance.DisableInput();
        StartCoroutine(UnpauseTimer());
    }
    
    private IEnumerator UnpauseTimer()
    {
        yield return new WaitForSeconds(pauseTimer);
        GameManager.Instance.uiManager.visualiserHUD.SetActive(false);
        GameManager.Instance.EnableInput();
    }
}
