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
    [SerializeField] int loadedChance = 26;

    [SerializeField] Sprite undamagedSprite;
    [SerializeField] Sprite damagedSprite;
    [SerializeField] SpriteRenderer crateSpriteRenderer;
    [SerializeField] CrateState myCrateState = CrateState.normal;

    public FMODUnity.EventReference explodeRef;

    enum CrateState
    {
        normal,
        alverium,
        medkit
    }

    private void Start()
    {
        switch (myCrateState){
            case CrateState.alverium:
                isLoaded = 1;
                break;

            case CrateState.medkit:
                isLoaded = 0;
                break;

            case CrateState.normal:
                int coinFlip = Random.Range(0, 100);
                if( coinFlip > loadedChance )
                {
                    isLoaded = 1;
                } else
                {
                    isLoaded = 0;
                }

                if( isLoaded == 1 )
                {
                    crateSpriteRenderer.sprite = damagedSprite;
                } else
                {
                    crateSpriteRenderer.sprite = undamagedSprite;
                }
                break;

            default:
                print("crate destructible start uh oh");
                break;
        }

        

    }

    public void Destroyed()
    {
        Instantiate(explosiveParticle, transform.position, Quaternion.identity);
        //FMODUnity.RuntimeManager.PlayOneShot(explodeRef);

        Instantiate(halfCrate, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

        if( isLoaded==1 )
        {
            GameObject go = Instantiate(alveriumPrefab,
                new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            HealthBase newbornHp = go.GetComponent<HealthBase>();
            go.transform.localScale = Vector3.one / 1.5f;
            go.GetComponent<AlveriumSoldier>().attackDamage = 10;
            //damage alverium
            newbornHp.HealthLevel = newbornHp.maxHealth / 2;
        }
        else if ( isLoaded == 0 ) 
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
