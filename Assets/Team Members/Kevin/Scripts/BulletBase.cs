using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float bulletDmg;
    public float bulletLife;

    private void OnEnable()
    {
	    rb = GetComponent<Rigidbody2D>();
	    rb.velocity = transform.right * speed;
	    Destroy(gameObject, bulletLife);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
	    EnemyBody enemy = col.gameObject.GetComponent<EnemyBody>();
	    if (enemy != null)
	    {
		    enemy.health.HealthLevel -= bulletDmg;
		    Destroy(gameObject);
	    }
	    else
	    {
		    Destroy(gameObject);
	    }
    }
}
