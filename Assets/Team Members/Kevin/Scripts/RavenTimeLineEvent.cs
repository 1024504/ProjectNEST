using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenTimeLineEvent : MonoBehaviour
{
    public GameManager instance;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
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
    }
}
