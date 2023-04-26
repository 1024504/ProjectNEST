using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteract : MonoBehaviour
{
    public GameObject interactUI;
    public void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            interactUI.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            interactUI.SetActive(false);
        }
    }
}
