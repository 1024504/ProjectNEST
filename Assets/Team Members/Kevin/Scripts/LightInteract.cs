using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteract : MonoBehaviour
{
    public InteractableObject trigger;
    public GameObject lightObjects;
    
    private void OnEnable()
    {
        if (trigger != null)
        {
            trigger.OnInteract += LightsOn;
        }
    }
    
    private void LightsOn()
    {
        lightObjects.SetActive(true);
    }
}
