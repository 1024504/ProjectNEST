using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour, IDestructable
{
    public int isLoaded = 0;
    public ParticleSystem explosiveParticle;
    public GameObject halfCrate;
    public GameObject alveriumPrefab;

    private void Start()
    {
        isLoaded = Random.Range(0, 2);
    }

    public void Destroyed()
    {
        //Instantiate(explosiveParticle, transform.position, Quaternion.identity);
        Instantiate(halfCrate, new Vector3(transform.position.x, transform.position.y / 2, transform.position.z), Quaternion.identity);
        if( isLoaded==1 )
        {
            Instantiate(alveriumPrefab, new Vector3(transform.position.x, transform.position.y / 2, transform.position.z), Quaternion.identity);
        }
        Destroy(gameObject);
        //animations and spawn logic
        //destroy function
    }
}
