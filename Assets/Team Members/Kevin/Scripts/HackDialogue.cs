using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Playables;

public class HackDialogue : MonoBehaviour
{
    public PlayableDirector timeLineDirector;
    public GameObject targetDoor;
    public void OnDisable()
    {
        if (targetDoor != null) targetDoor.SetActive(false);
        if (timeLineDirector != null) timeLineDirector.Play();
    }
}
