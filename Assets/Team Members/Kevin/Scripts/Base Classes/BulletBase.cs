using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class BulletBase : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float bulletDmg;
    public float bulletLife;
    public Transform owner;

    private void OnEnable()
    {
	    rb = GetComponent<Rigidbody2D>();
	    rb.velocity = transform.right * speed;
	    Destroy(gameObject, bulletLife);
    }

    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
	    OnHit(col);
    }

    public virtual void OnHit(Collision2D col)
    {
	    HealthBase enemyHealth = col.gameObject.GetComponentInParent<HealthBase>();
	    if (enemyHealth != null)
	    {
		    enemyHealth.HealthLevel -= bulletDmg;
		    Destroy(gameObject);
	    }
	    else
	    {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Gun SFX/BulletImpacts/Bullet_Metal_Impact");
            Destroy(gameObject);
	    }
    }
}
