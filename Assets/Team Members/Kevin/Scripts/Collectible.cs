using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Collectibles
{
    public FMODUnity.EventReference collectibleDialogue;
    public MyCollectible myCollectible;
    private void OnTriggerEnter2D(Collider2D col)
    {
        CollectiblesBag bag = col.GetComponent<CollectiblesBag>();
        if (bag != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot(collectibleDialogue);
            bag.UpdateBag(myCollectible);
            Destroy(gameObject);
        }
    }
}
