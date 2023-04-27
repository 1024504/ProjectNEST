using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectiblesPanelManager : MonoBehaviour
{
	public List<MyCollectibleGameObjectPair> collectibles = new ();
    // public GameObject plaza1;
    // public GameObject plaza2;
    // public GameObject plaza3;
    // public GameObject res1;
    // public GameObject res2;
    // public GameObject res3;
    // public GameObject bio1;
    // public GameObject bio2;
    // public GameObject bio3;
    
    public CollectiblesBag colBag;
    
    public void OnEnable()
    {
        colBag = GameManager.Instance.playerPrefab.GetComponentInChildren<CollectiblesBag>();
    }
    
    /*public void OnCollectedUpdate()
    {
        if (colBag.hasPlaza1) plaza1.SetActive(true);
        if (colBag.hasPlaza2) plaza2.SetActive(true);
        if (colBag.hasPlaza3) plaza3.SetActive(true);
        if (colBag.hasRes1) res1.SetActive(true);
        if (colBag.hasRes2) res2.SetActive(true);
        if (colBag.hasRes3) res3.SetActive(true);
        if (colBag.hasBio1) bio1.SetActive(true);
        if (colBag.hasBio2) bio2.SetActive(true);
        if (colBag.hasBio3) bio3.SetActive(true);
    }*/
    public void OnCollectedUpdate()
    {
	    for (int i = 0; i < colBag.hasCollectibles.Count; i++)
	    {
		    if (!colBag.hasCollectibles[i].isCollected) continue;
		    foreach (MyCollectibleGameObjectPair collectibleGameObjectPair in collectibles)
		    {
			    if (collectibleGameObjectPair.collectible != (MyCollectible) i) continue;
			    collectibleGameObjectPair.gameObject.SetActive(true);
		    }
	    }
	    
	    // if (GameManager.Instance.playerController.GetComponent<CollectiblesBag>().hasPlaza1) plaza1.SetActive(true);
        // if (GameManager.Instance.playerController.GetComponent<CollectiblesBag>().hasPlaza2) plaza2.SetActive(true);
        // if (GameManager.Instance.playerController.GetComponent<CollectiblesBag>().hasPlaza3) plaza3.SetActive(true);
        // if (GameManager.Instance.playerController.GetComponent<CollectiblesBag>().hasRes1) res1.SetActive(true);
        // if (GameManager.Instance.playerController.GetComponent<CollectiblesBag>().hasRes2) res2.SetActive(true);
        // if (GameManager.Instance.playerController.GetComponent<CollectiblesBag>().hasRes3) res3.SetActive(true);
        // if (GameManager.Instance.playerController.GetComponent<CollectiblesBag>().hasBio1) bio1.SetActive(true);
        // if (GameManager.Instance.playerController.GetComponent<CollectiblesBag>().hasBio2) bio2.SetActive(true);
        // if (GameManager.Instance.playerController.GetComponent<CollectiblesBag>().hasBio3) bio3.SetActive(true);
    }

}
