using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public InteractionEventManager interactionEventManager;

    public void Awake()
    {
        interactionEventManager = GetComponentInParent<InteractionEventManager>();
    }
    public void OnEnable()
    {
        interactionEventManager.onEventTriggered += DoStuff;
    }

    public void OnDisable()
    {
        interactionEventManager.onEventTriggered -= DoStuff;
    }

    private void DoStuff()
    {
        gameObject.SetActive(false);
    }
}
