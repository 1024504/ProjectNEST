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
    [SerializeField] private GameObject pauseMenu;
    public Player player;
    [SerializeField] private Rifle _rifle;
    [SerializeField] private Shotgun _shotgun;
    [SerializeField] private Sniper _sniper;

    [SerializeField] private TextMeshProUGUI rifleAmmoText;
    [SerializeField] private TextMeshProUGUI shotgunAmmoText;
    [SerializeField] private TextMeshProUGUI sniperAmmoText;
    public void Awake()
    {
        player = GameManager.Instance.playerPrefabRef.GetComponent<Player>();
        _rifle = player.GetComponentInChildren<Rifle>();
        _shotgun = player.GetComponentInChildren<Shotgun>();
        _sniper = player.GetComponentInChildren<Sniper>();
    }
    
    
    #region UI Update
    public void OnEnable()
    {
        _rifle.OnShoot += UpdateRifleAmmo; 
    }

    public void OnDisable()
    {
        _rifle.OnShoot -= UpdateRifleAmmo;
    }

    private void UpdateRifleAmmo()
    {
        rifleAmmoText.SetText(_rifle.currentMagazine.ToString());
    }

    #endregion
    public void Pause()
    {
        GameManager.Instance.gamePaused = true;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    
    public void ResumeButton()
    {
        GameManager.Instance.gamePaused = false;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TestDemoMainMenu");
    }
}
