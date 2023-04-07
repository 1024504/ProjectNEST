using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelDoors : InteractableObject
{
    public Transform connectingDoor;

    private bool _doorDelay;
    //private Vector3 _teleportOffset;

    public void Awake()
    {
        //_teleportOffset = new Vector3(6f, 0, 0);
    }

    protected override void Interact(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && !_doorDelay)
        {
            _doorDelay = true;
            player.interactButtonPressed = false;
            other.gameObject.transform.position = connectingDoor.position;
            StartCoroutine(DoorCooldown());
        }
    }

    private IEnumerator DoorCooldown()
    {
        yield return new WaitForSeconds(1f);
        _doorDelay = false;
    }
    

    /*public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            other.gameObject.transform.position = connectingDoor.position + _teleportOffset;
        }
    }*/
}
