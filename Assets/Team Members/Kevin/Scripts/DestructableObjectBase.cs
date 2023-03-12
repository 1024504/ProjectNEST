using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjectBase : MonoBehaviour, IDestructable
{
    [SerializeField] private int hitCounter;
    public ParticleSystem explosiveParticle;

    private void OnCollisionEnter2D(Collision2D col)
    {
        Bullet otherBullet = col.gameObject.GetComponent<Bullet>();
        if (otherBullet != null)
        {
            hitCounter--;
            if (hitCounter < 1) Destroyed();
        }
    }

    public void Destroyed()
    {
        Instantiate(explosiveParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //animations and spawn logic
        //destroy function
    }
}
