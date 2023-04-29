using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenTimeLineEvent : MonoBehaviour
{
    private GameManager instance;

    public void OnEnable()
    {
        instance = GameManager.Instance;
    }
    public void EnableBool()
    {
        instance.playerController.GetComponent<Player>().hasSniper = true;
        instance.playerController.GetComponent<Player>().grappleEnabled = true;
    }

    public void ChangeSniperUI()
    {
        instance.uiManager.TurnOnSniperHUD();
        instance.uiManager.TurnOnGrappleHUD();
    }
}
