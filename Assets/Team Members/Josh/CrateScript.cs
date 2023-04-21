using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour, IDestructable
{
    public int isLoaded = 0;
    public ParticleSystem explosiveParticle;
    public GameObject halfCrate;
    public GameObject alveriumPrefab;

    public FMODUnity.EventReference explodeRef;

    private void Start()
    {
        isLoaded = Random.Range(0, 2);
    }

    public void Destroyed()
    {
        Instantiate(explosiveParticle, transform.position, Quaternion.identity);
        FMODUnity.RuntimeManager.PlayOneShot(explodeRef);

        Instantiate(halfCrate, new Vector3(transform.position.x, transform.position.y / 2, transform.position.z), Quaternion.identity);
        if( isLoaded==1 )
        {
            HealthBase newbornHp = Instantiate(alveriumPrefab, new Vector3(transform.position.x, transform.position.y / 2, transform.position.z), Quaternion.identity).GetComponent<HealthBase>();
            newbornHp.HealthLevel = newbornHp.maxHealth / 2;
            //damage alverium
        }
        //aggro nearby alverium

        Destroy(gameObject);
    }

    public void TrapTriggered()
    {
        Instantiate(explosiveParticle, transform.position, Quaternion.identity);
        Instantiate(halfCrate, new Vector3(transform.position.x, transform.position.y / 2, transform.position.z), Quaternion.identity);
        Instantiate(alveriumPrefab, new Vector3(transform.position.x, transform.position.y / 2, transform.position.z), Quaternion.identity);
    }
}
