using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesBag : MonoBehaviour
{
	public List<MyCollectibleBoolPair> hasCollectibles = new ();
    // public bool hasPlaza1;
    // public bool hasPlaza2;
    // public bool hasPlaza3;
    //
    // public bool hasRes1;
    // public bool hasRes2;
    // public bool hasRes3;
    //
    // public bool hasBio1;
    // public bool hasBio2;
    // public bool hasBio3;

    public void UpdateBag(MyCollectible collectibles)
    {
	    foreach (MyCollectibleBoolPair hasCollectible in hasCollectibles)
	    {
		    if (hasCollectible.collectible != collectibles) continue;
		    hasCollectible.isCollected = true;
	    }

        // if (collectibles == Collectibles.MyCollectible.Plaza1)hasPlaza1 = true;
        // else if (collectibles == Collectibles.MyCollectible.Plaza2)hasPlaza2 = true;
        // else if (collectibles == Collectibles.MyCollectible.Plaza3)hasPlaza3 = true;
        // else if (collectibles == Collectibles.MyCollectible.ResArea1)hasRes1 = true;
        // else if (collectibles == Collectibles.MyCollectible.ResArea2)hasRes2 = true;
        // else if (collectibles == Collectibles.MyCollectible.ResArea3)hasRes3 = true;
        // else if (collectibles == Collectibles.MyCollectible.Bio1)hasBio1 = true;
        // else if (collectibles == Collectibles.MyCollectible.Bio2)hasBio2 = true;
        // else if (collectibles == Collectibles.MyCollectible.Bio3)hasBio3 = true;
        
    }
}
