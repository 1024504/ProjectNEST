using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour, IDestructable
{
    public int isLoaded = 0;
    public ParticleSystem explosiveParticle;
    public GameObject halfCrate;
    public GameObject alveriumPrefab;
    public GameObject medkitPrefab;

    public FMODUnity.EventReference explodeRef;

    private void Start()
    {
        isLoaded = Random.Range(0, 2);
    }

    public void Destroyed()
    {
        Instantiate(explosiveParticle, transform.position, Quaternion.identity);
        //FMODUnity.RuntimeManager.PlayOneShot(explodeRef);

        Instantiate(halfCrate, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        int coinFlip = Random.Range(0, 100);
        if(coinFlip > 25)
        {
            GameObject go = Instantiate(alveriumPrefab,
                new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            HealthBase newbornHp = go.GetComponent<HealthBase>();
            go.transform.localScale = Vector3.one / 1.5f;
            go.GetComponent<AlveriumSoldier>().attackDamage = 10;
            newbornHp.HealthLevel = newbornHp.maxHealth / 2;
            //damage alverium
        }
        else if (coinFlip <= 24) 
        {
            Instantiate(medkitPrefab, transform.position, Quaternion.identity);
        }
            
        //aggro nearby alverium

        Destroy(gameObject);
    }

    public void TrapTriggered()
    {
        Instantiate(explosiveParticle, transform.position, Quaternion.identity);
        //FMODUnity.RuntimeManager.PlayOneShot(explodeRef);

        Instantiate(halfCrate, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        GameObject go = Instantiate(alveriumPrefab,
            new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        HealthBase newbornHp = go.GetComponent<HealthBase>();
        go.transform.localScale = Vector3.one /  1.5f;
        go.GetComponent<AlveriumSoldier>().attackDamage = 10;
        newbornHp.HealthLevel = newbornHp.maxHealth / 2;
        Destroy(gameObject);
    }
}
