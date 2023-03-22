using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public bool onTarget = false;
    
    public void OnTriggerStay2D(Collider2D col)
    {
        AlveriumSoldier enemy = col.GetComponent<AlveriumSoldier>();
        if (enemy != null)
        {
            onTarget = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        onTarget = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);
    }
    
    public void Update()
    {
        if (onTarget)
        {
            gameObject.transform.rotation =
                Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -45f), 10f);
            gameObject.transform.localScale = new Vector3(0.0630299002f, 0.0630299002f, 0.0630299002f) * 2;
            
            
        }
        else if(!onTarget)
        {
            gameObject.transform.rotation =
                Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(0, 0, 0) , 10f);
            gameObject.transform.localScale = new Vector3(0.0630299002f, 0.0630299002f, 0.0630299002f);
        }
    }
    
}
