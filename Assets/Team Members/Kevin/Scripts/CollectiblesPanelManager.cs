using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesPanelManager : MonoBehaviour
{
    public GameObject plaza1;
    public GameObject plaza2;
    public GameObject plaza3;
    public GameObject res1;
    public GameObject res2;
    public GameObject res3;
    public GameObject bio1;
    public GameObject bio2;
    public GameObject bio3;

    public Player player;
    public GameManager instance;
    public void Start()
    {
        instance = GameManager.Instance;
    }
    public void OnCollectedUpdate()
    {
        if (player.GetComponent<CollectiblesBag>().hasPlaza1) plaza1.SetActive(true);
        if (player.GetComponent<CollectiblesBag>().hasPlaza2) plaza2.SetActive(true);
        if (player.GetComponent<CollectiblesBag>().hasPlaza3) plaza3.SetActive(true);
        if (player.GetComponent<CollectiblesBag>().hasRes1) res1.SetActive(true);
        if (player.GetComponent<CollectiblesBag>().hasRes2) res2.SetActive(true);
        if (player.GetComponent<CollectiblesBag>().hasRes3) res3.SetActive(true);
        if (player.GetComponent<CollectiblesBag>().hasBio1) bio1.SetActive(true);
        if (player.GetComponent<CollectiblesBag>().hasBio2) bio2.SetActive(true);
        if (player.GetComponent<CollectiblesBag>().hasBio3) bio3.SetActive(true);
    }

}
