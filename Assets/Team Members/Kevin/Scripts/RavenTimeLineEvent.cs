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
        instance.GetComponentInChildren<SniperUI>().gameObject.transform.localScale = Vector3.one;
        instance.GetComponentInChildren<GrappleUI>().gameObject.transform.localScale = new Vector3(0.381142586f, 0.609828174f, 0.609828174f);
    }
}
