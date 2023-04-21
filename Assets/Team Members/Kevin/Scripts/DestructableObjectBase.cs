using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjectBase : MonoBehaviour
{
    [SerializeField] private int hitCounter;

    private void OnCollisionEnter2D(Collision2D col)
    {
        Bullet otherBullet = col.gameObject.GetComponent<Bullet>();
        if (otherBullet != null)
        {
            hitCounter--;
            if (hitCounter == 1) this.GetComponentInParent<CrateScript>().Destroyed();
        }
    }
}
