using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public InteractionEvent interactionEvent;

    public void Awake()
    {
        interactionEvent = GetComponentInParent<InteractionEvent>();
    }
    public void OnEnable()
    {
        interactionEvent.onEventTriggered += DoStuff;
    }

    public void OnDisable()
    {
        interactionEvent.onEventTriggered -= DoStuff;
    }

    private void DoStuff()
    {
        gameObject.SetActive(false);
    }
}
