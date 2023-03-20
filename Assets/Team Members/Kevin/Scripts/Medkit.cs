using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null && player.medkitCount < player.maxMedkit)
        {
            player.PickUp();
            Destroy(gameObject);
        }
    }
}
