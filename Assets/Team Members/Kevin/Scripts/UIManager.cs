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
    public PlayerController playerController;
    [SerializeField] private Rifle _rifle;
    [SerializeField] private Shotgun _shotgun;
    [SerializeField] private Sniper _sniper;

    [SerializeField] private TextMeshProUGUI rifleAmmoText;
    [SerializeField] private TextMeshProUGUI shotgunAmmoText;
    [SerializeField] private TextMeshProUGUI sniperAmmoText;
    [SerializeField] private TextMeshProUGUI medKitText;
    public void Awake()
    {
        player = GameManager.Instance.playerPrefabRef.GetComponent<Player>();
        playerController = GameManager.Instance.playerPrefabRef.GetComponent<PlayerController>();
        _rifle = player.weaponsList[0].GetComponent<Rifle>();
        _shotgun = player.weaponsList[1].GetComponent<Shotgun>();
        _sniper = player.weaponsList[2].GetComponent<Sniper>();
    }
    
    
    #region UI Update
    public void OnEnable()
    {
        _rifle.OnShoot += UpdateRifleAmmo;
        _shotgun.OnShoot += UpdateShotGunAmmo;
        _sniper.OnShoot += UpdateSniperAmmo;
        player.OnPickUp += UpdateMedKitCount;
    }

    public void OnDisable()
    {
        _rifle.OnShoot -= UpdateRifleAmmo;
        _shotgun.OnShoot -= UpdateShotGunAmmo;
        _sniper.OnShoot -= UpdateSniperAmmo;
        player.OnPickUp -= UpdateMedKitCount;
    }

    private void UpdateRifleAmmo()
    {
        rifleAmmoText.SetText(_rifle.currentMagazine.ToString());
    }

    private void UpdateShotGunAmmo()
    {
        shotgunAmmoText.SetText(_shotgun.currentMagazine.ToString());
    }

    private void UpdateSniperAmmo()
    {
        sniperAmmoText.SetText(_sniper.currentMagazine.ToString());
    }

    private void UpdateMedKitCount()
    {
        medKitText.SetText(player.medkitCount.ToString());
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
