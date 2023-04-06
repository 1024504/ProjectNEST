using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoors : MonoBehaviour
{
    public Transform connectingDoor;
    private Vector3 _teleportOffset;

    public void Awake()
    {
        _teleportOffset = new Vector3(6f, 0, 0);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            other.gameObject.transform.position = connectingDoor.position + _teleportOffset;
        }
    }
}
