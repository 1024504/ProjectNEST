using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class HackDialogue : MonoBehaviour
{
    public EventReference dialogue;
    private void OnDisable()
    {
        RuntimeManager.PlayOneShot(dialogue);
    }
}
