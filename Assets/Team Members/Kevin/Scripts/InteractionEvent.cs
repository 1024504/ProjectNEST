using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public delegate void OnInteract();
    public event OnInteract onEventTriggered;
    private void OnTriggerStay2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null && GameManager.Instance.interactButtonPressed)
        {
            onEventTriggered?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
