using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HawkTimelineEvents : MonoBehaviour
{
    public GameManager instance;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        instance = GameManager.Instance;
    }
    
    public void EnableTimeLineBools()
    {
        instance.playerController.GetComponent<Player>().doubleJumpEnabled = true;
        instance.playerController.GetComponent<Player>().hasShotgun = true;
    }

    public void ChangeShotgunUI()
    {
        instance.GetComponentInChildren<ShotgunUI>().gameObject.transform.localScale = Vector3.one;
    }
}
