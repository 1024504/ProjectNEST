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
    public static UIManager UIInstance { get; private set; }
    public GameManager instance;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private TextMeshProUGUI medKitText;
    
    public Player player;
    public PlayerController playerController;
    public Transform respawnPoint;
    public PlayerHealth playerHealth;
    
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
    
    //Objectives HUD
    [Header("Objectives HUD")] 
    public GameObject objectivesMarker;
    public Transform branchProceduralPanel;
    public TextMeshProUGUI objectiveText;
    public Transform textProceduralPanel;
    

    public GameObject aboveHeadUI;

    public Color noAlpha;
    public Color halfAlpha;
    public Color fullAlpha;
    
    public void Awake()
    {
        if (UIInstance == null)
        {
            UIInstance = this;
            //Debug.Log("UI Manager = NULL!");
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        instance = GameManager.Instance;
        
        //player = GameManager.Instance.playerPrefabRef.GetComponent<Player>();
        //playerController = GameManager.Instance.playerPrefabRef.GetComponent<PlayerController>();
        playerHealth = player.GetComponent<PlayerHealth>();
        _rifle = player.weaponsList[0].GetComponent<Rifle>();
        _shotgun = player.weaponsList[1].GetComponent<Shotgun>();
        _sniper = player.weaponsList[2].GetComponent<Sniper>();
        
        noAlpha = new Color(0, 0, 0, 0f);
        halfAlpha = new Color(0, 0, 0, 0.5f);
        fullAlpha = new Color(0, 0, 0, 1);
        UpdateWeaponHUD();

    }

    #region Objectives HUD
    public void UpdateObjectives()
    {
        GameObject go = Instantiate(objectivesMarker, branchProceduralPanel.position, Quaternion.identity);
        go.transform.SetParent(branchProceduralPanel.transform, false);

        TextMeshProUGUI textGO = Instantiate(objectiveText, textProceduralPanel.position, Quaternion.identity);
        textGO.transform.SetParent(textProceduralPanel.transform, false);
        if (instance.gameObject != null)
        {
            textGO.text = instance.objectives[instance.currentMission];
        }
    }

    #endregion
    
    #region UI Update

    public void OnEnable()
    {
        _rifle.OnShoot += UpdateRifleAmmo;
        _shotgun.OnShoot += UpdateShotGunAmmo;
        _sniper.OnShoot += UpdateSniperAmmo;
        player.OnPickUp += UpdateMedKitCount;
        player.OnGunSwitch += UpdateWeaponHUD;
        UpdateWeaponHUD();
        if (playerHealth == null) return;
        player.GetComponent<PlayerHealth>().OnDeath += ActiveDeathMenu;
        instance.InteractionEventManager.onEventTriggered += UpdateObjectives;
    }

    public void OnDisable()
    {
        _rifle.OnShoot -= UpdateRifleAmmo;
        _shotgun.OnShoot -= UpdateShotGunAmmo;
        _sniper.OnShoot -= UpdateSniperAmmo;
        player.OnPickUp -= UpdateMedKitCount;
        player.OnGunSwitch -= UpdateWeaponHUD;
        if (playerHealth == null) return;
        player.GetComponent<PlayerHealth>().OnDeath -= ActiveDeathMenu;
        instance.InteractionEventManager.onEventTriggered -= UpdateObjectives;
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
        instance.gamePaused = true;
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

        rifleHUD.gameObject.GetComponent<Image>().color = Color.white;
        shotgunHUD.gameObject.GetComponent<Image>().color = noAlpha;
        sniperHUD.gameObject.GetComponent<Image>().color = noAlpha;
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
        
        rifleHUD.gameObject.GetComponent<Image>().color = noAlpha;
        shotgunHUD.gameObject.GetComponent<Image>().color = Color.white;
        sniperHUD.gameObject.GetComponent<Image>().color = noAlpha;
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
        
        rifleHUD.gameObject.GetComponent<Image>().color = noAlpha;
        shotgunHUD.gameObject.GetComponent<Image>().color = noAlpha;
        sniperHUD.gameObject.GetComponent<Image>().color = Color.white;
    }

    #endregion
    

    #endregion
    public void Pause()
    {
        instance.gamePaused = true;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    
    public void ResumeButton()
    {
        instance.gamePaused = false;
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
        instance.gamePaused = false;
        instance.GameReset();      
        Cursor.visible = false;
        //load last checkpoint
    }
}
