using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Playables;

public class HackDialogue : MonoBehaviour
{
    public PlayableDirector timeLineDirector;
    
    public void OnDisable()
    {
        if (timeLineDirector != null) timeLineDirector.Play();
    }
}
