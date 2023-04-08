using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjectBase : MonoBehaviour, IDestructable
{
    [SerializeField] private int hitCounter;
    public ParticleSystem explosiveParticle;
    public GameObject halfCrate;
    public GameObject alveriumPrefab;

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
        //Instantiate(explosiveParticle, transform.position, Quaternion.identity);
        Instantiate(halfCrate, new Vector3(transform.position.x,transform.position.y/2,transform.position.z), Quaternion.identity);
        Instantiate(alveriumPrefab, new Vector3(transform.position.x,transform.position.y/2,transform.position.z), Quaternion.identity);
        Destroy(gameObject);
        //animations and spawn logic
        //destroy function
    }
}
