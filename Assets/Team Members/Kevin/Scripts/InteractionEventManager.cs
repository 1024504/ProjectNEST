using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEventManager : MonoBehaviour
{
    public delegate void OnInteract();
    public event OnInteract onEventTriggered;

    public FMODUnity.EventReference interactSFX;

    public int missionInt;
    private void OnTriggerStay2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null && GameManager.Instance.interactButtonPressed)
        {
            FMODUnity.RuntimeManager.PlayOneShot(interactSFX);
            GameManager.Instance.currentMission++;
            onEventTriggered?.Invoke();
            gameObject.SetActive(false);
        }
    }

    
}
