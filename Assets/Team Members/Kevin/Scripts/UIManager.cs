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
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private TextMeshProUGUI medKitText;
    
    public Player player;
    public PlayerController playerController;
    public Transform respawnPoint;
    
    //Rifle HUD
    [Header("Rifle HUD")]
    [SerializeField] private Rifle _rifle;
    [SerializeField] private TextMeshProUGUI rifleAmmoText;
    [SerializeField] private TextMeshProUGUI rifleMaxAmmoText;
    [SerializeField] private RawImage rifleIMG;
    [SerializeField] private GameObject rifleHUD;

    //ShotGun HUD
    [Header("Shotgun HUD")]
    [SerializeField] private Shotgun _shotgun;
    [SerializeField] private TextMeshProUGUI shotgunAmmoText;
    [SerializeField] private TextMeshProUGUI shotgunMaxAmmoText;
    [SerializeField] private RawImage shotgunIMG;
    [SerializeField] private GameObject shotgunHUD;

    //Sniper HUD
    [Header("Sniper HUD")]
    [SerializeField] private Sniper _sniper;
    [SerializeField] private TextMeshProUGUI sniperAmmoText;
    [SerializeField] private TextMeshProUGUI sniperMaxAmmoText;
    [SerializeField] private RawImage sniperIMG;
    [SerializeField] private GameObject sniperHUD;

    public Color halfAlpha;
    public Color fullAlpha;
    public void Awake()
    {
        //player = GameManager.Instance.playerPrefabRef.GetComponent<Player>();
        //playerController = GameManager.Instance.playerPrefabRef.GetComponent<PlayerController>();
        _rifle = player.weaponsList[0].GetComponent<Rifle>();
        _shotgun = player.weaponsList[1].GetComponent<Shotgun>();
        _sniper = player.weaponsList[2].GetComponent<Sniper>();
        halfAlpha = new Color(0, 0, 0, 0.5f);
        fullAlpha = new Color(0, 0, 0, 1);

    }
    
    #region UI Update

    public void OnEnable()
    {
        _rifle.OnShoot += UpdateRifleAmmo;
        _shotgun.OnShoot += UpdateShotGunAmmo;
        _sniper.OnShoot += UpdateSniperAmmo;
        player.OnPickUp += UpdateMedKitCount;
        player.OnGunSwitch += UpdateWeaponHUD;
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;
        player.GetComponent<PlayerHealth>().OnDeath += ActiveDeathMenu;
    }

    public void OnDisable()
    {
        _rifle.OnShoot -= UpdateRifleAmmo;
        _shotgun.OnShoot -= UpdateShotGunAmmo;
        _sniper.OnShoot -= UpdateSniperAmmo;
        player.OnPickUp -= UpdateMedKitCount;
        player.OnGunSwitch -= UpdateWeaponHUD;
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;
        player.GetComponent<PlayerHealth>().OnDeath -= ActiveDeathMenu;
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

    private void ActiveDeathMenu()
    {
        deathMenu.SetActive(true);
        GameManager.Instance.gamePaused = true;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    private void UpdateWeaponHUD()
    {
        if (player.currentWeapon.GetComponent<Rifle>())
        {
            RifleHUD();
        }
        else if (player.currentWeapon.GetComponent<Shotgun>())
        {
            ShotgunHUD();
        }
        else if (player.currentWeapon.GetComponent<Sniper>())
        {
            SniperHUD();
        }
    }

    #region GUNHUD

    private void RifleHUD()
    {
        rifleAmmoText.color = fullAlpha;
        rifleMaxAmmoText.color = fullAlpha;
        rifleIMG.color = fullAlpha;
        
        shotgunAmmoText.color = halfAlpha;
        shotgunMaxAmmoText.color = halfAlpha;
        shotgunIMG.color = halfAlpha;
        
        sniperAmmoText.color = halfAlpha;
        sniperMaxAmmoText.color = halfAlpha;
        sniperIMG.color = halfAlpha;

        rifleHUD.gameObject.GetComponent<Image>().color = Color.yellow;
        shotgunHUD.gameObject.GetComponent<Image>().color = Color.white;
        sniperHUD.gameObject.GetComponent<Image>().color = Color.white;
    }

    private void ShotgunHUD()
    {
        rifleAmmoText.color = halfAlpha;
        rifleMaxAmmoText.color = halfAlpha;
        rifleIMG.color = halfAlpha;
        
        shotgunAmmoText.color = fullAlpha;
        shotgunMaxAmmoText.color = fullAlpha;
        shotgunIMG.color = fullAlpha;
        
        sniperAmmoText.color = halfAlpha;
        sniperMaxAmmoText.color = halfAlpha;
        sniperIMG.color = halfAlpha;
        
        rifleHUD.gameObject.GetComponent<Image>().color = Color.white;
        shotgunHUD.gameObject.GetComponent<Image>().color = Color.yellow;
        sniperHUD.gameObject.GetComponent<Image>().color = Color.white;
    }

    private void SniperHUD()
    {
        rifleAmmoText.color = halfAlpha;
        rifleMaxAmmoText.color = halfAlpha;
        rifleIMG.color = halfAlpha;
        
        shotgunAmmoText.color = halfAlpha;
        shotgunMaxAmmoText.color = halfAlpha;
        shotgunIMG.color = halfAlpha;
        
        sniperAmmoText.color = fullAlpha;
        sniperMaxAmmoText.color = fullAlpha;
        sniperIMG.color = fullAlpha;
        
        rifleHUD.gameObject.GetComponent<Image>().color = Color.white;
        shotgunHUD.gameObject.GetComponent<Image>().color = Color.white;
        sniperHUD.gameObject.GetComponent<Image>().color = Color.yellow;
    }

    #endregion
    

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
        deathMenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryButton()
    {
        deathMenu.SetActive(false);
        GameManager.Instance.GameReset();
        Debug.Log("Retry!");
        //load last checkpoint
    }
}
