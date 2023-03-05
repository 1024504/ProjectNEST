using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Player player;
    public GameObject rifle;
    public GameObject shotgun;
    public GameObject sniper;
    public TextMeshProUGUI rifleAmmoText;
    public TextMeshProUGUI shotgunAmmoText;
    public TextMeshProUGUI sniperAmmoText;
    public TextMeshProUGUI medkitAmountText;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI playerHPAmountText;
    //public Slider hpSlider;

    public void Update()
    {
        //place holder until events are added
        rifleAmmoText.text = rifle.GetComponent<Rifle>().currentMagazine.ToString();
        shotgunAmmoText.text = shotgun.GetComponent<Shotgun>().currentMagazine.ToString();
        sniperAmmoText.text = sniper.GetComponent<Sniper>().currentMagazine.ToString();
        medkitAmountText.text = player.medkitCount.ToString();
        playerHPAmountText.text = player.GetComponent<PlayerHealth>().HealthLevel.ToString();
        killCountText.text = GameManager.Instance.killCount.ToString();
        //hpSlider.value = player.GetComponent<Health>().HealthLevel;
    }
    
    [SerializeField] private GameObject pauseMenu;
    public void Pause()
    {
        GameManager.Instance.gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        GameManager.Instance.gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TestDemoMainMenu");
    }
    
}
