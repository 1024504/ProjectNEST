using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //place holder until events are added
        rifleAmmoText.text = rifle.GetComponent<Rifle>().currentMagazine.ToString();
        shotgunAmmoText.text = shotgun.GetComponent<Shotgun>().currentMagazine.ToString();
        sniperAmmoText.text = sniper.GetComponent<Sniper>().currentMagazine.ToString();
    }
    
    [SerializeField] private GameObject pauseMenu;
    public static bool IsPaused = false;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TestDemoMainMenu");
    }
    
}
