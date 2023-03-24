using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public FMODUnity.EventReference pickUpRef;
    public FMODUnity.EventReference emoteRef;

    public void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null && player.medkitCount < player.maxMedkit)
        {
            StartCoroutine(EmoteDelay());
            FMODUnity.RuntimeManager.PlayOneShot(emoteRef);                         
            player.PickUp();
            gameObject.transform.position = new Vector2(937, 123);
            Destroy(gameObject, 1f);
        }
    }

    public IEnumerator EmoteDelay()
    {
        yield return new WaitForSeconds(.5f);
        FMODUnity.RuntimeManager.PlayOneShot(pickUpRef);
    }
}
