using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public delegate void OnInteract();
    public event OnInteract onEventTriggered;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            onEventTriggered?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
