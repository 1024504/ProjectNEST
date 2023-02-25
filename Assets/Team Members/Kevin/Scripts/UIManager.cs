using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Player player;
    public GameObject rifle;
    public GameObject shotgun;
    public GameObject sniper;
    public TextMeshProUGUI rifleAmmoText;
    public TextMeshProUGUI shotgunAmmoText;
    public TextMeshProUGUI sniperAmmoText;

    public void Update()
    {
        rifleAmmoText.text = rifle.GetComponent<Rifle>().currentMagazine.ToString();
        shotgunAmmoText.text = shotgun.GetComponent<Shotgun>().currentMagazine.ToString();
        sniperAmmoText.text = sniper.GetComponent<Sniper>().currentMagazine.ToString();
    }
}
