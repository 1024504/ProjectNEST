using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public void OnTriggerStay2D(Collider2D col)
    {
        AlveriumSoldier enemy = col.GetComponent<AlveriumSoldier>();
        if (enemy != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        /*EnemyBody enemy = other.GetComponent<EnemyBody>();
        if (enemy != null)
        {
            
        }*/
    }
}
