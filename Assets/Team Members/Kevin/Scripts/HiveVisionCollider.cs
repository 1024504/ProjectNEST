using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveVisionCollider : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            gameObject.GetComponentInParent<Hive>().SpawnMoth();
        }
    }
}
