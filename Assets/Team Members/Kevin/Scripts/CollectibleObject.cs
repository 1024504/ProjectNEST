using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CollectibleObject : MonoBehaviour
{
    public FMODUnity.EventReference collectibleDialogue;
    public MyCollectible myCollectible;

    [SerializeField] GameObject promptObject;
    [SerializeField] TextMeshProUGUI promptText;

    private void OnEnable()
    {
	    GameManager.Instance.OnFinishLoading += CheckAlreadyCollected;
	    LevelManager.Instance.OnSceneLoaded += CheckAlreadyCollected;
    }

    private void OnDisable()
    {
	    GameManager.Instance.OnFinishLoading -= CheckAlreadyCollected;
	    LevelManager.Instance.OnSceneLoaded -= CheckAlreadyCollected;
    }

    private void CheckAlreadyCollected()
    {
	    foreach (MyCollectibleBoolPair collectible in GameManager.Instance.saveData.collectibles)
	    {
		    if (collectible.collectible != myCollectible) continue;
		    if (collectible.isCollected)
		    {
			    gameObject.SetActive(false);
		    }
		    break;
	    }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        CollectiblesBag bag = col.GetComponent<CollectiblesBag>();
        if (bag != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot(collectibleDialogue);
            bag.UpdateBag(myCollectible);

            promptObject.SetActive(true);
            promptText.text = "Collectible obtained: " + gameObject.name;
            gameObject.SetActive(false);
        }
    }
}
