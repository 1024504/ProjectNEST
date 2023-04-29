using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameManager gm;
    
    [Header("Direct References")]
    public GameObject aboveHeadUI;
    public GameObject visualiserHUD;
    public GameObject hUDGameObject;
    public GameObject resumeButton;
    public GameObject respawnButton;
    public GameObject collectibleUI;
    public GameObject controlsUI;
    public GameObject settingsUI;

    [Header("Space")]
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
    
    //Save UI
    public Animator saveIconUIAnimator;
    public AnimationClip saveIn;
    public AnimationClip saveSpin;
    public AnimationClip saveOut;

    [Header("Collectible Audio")] 
    public List<FMODUnity.EventReference> audioIndex;
    //public FMODUnity.EventReference playPlaza1;
    
    //Colours
    public Color noAlpha;
    public Color halfAlpha;
    public Color fullAlpha;
    
    public GameObject generalOptionsButton;
    public GameObject audioOptionsButton;
    public GameObject monitorOptionsButton;
    
    public void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        gm = GetComponentInParent<GameManager>();
        
        //player = GameManager.Instance.playerPrefabRef.GetComponent<Player>();
        //playerController = GameManager.Instance.playerPrefabRef.GetComponent<PlayerController>();
        

    }

    public void SubscribeToPlayerEvents()
    {
	    player.OnReload += OnPlayerReload;
	    
	    playerHealth = player.GetComponent<PlayerHealth>();
	    _rifle = player.weaponsList[0].GetComponent<Rifle>();
	    _shotgun = player.weaponsList[1].GetComponent<Shotgun>();
	    _sniper = player.weaponsList[2].GetComponent<Sniper>();
	    aboveHeadUI = player.GetComponentInChildren<AboveHeadUI>().gameObject;
	    aboveHeadUI.SetActive(false);
        
	    noAlpha = new Color(0, 0, 0, 0f);
	    halfAlpha = new Color(0, 0, 0, 0.5f);
	    fullAlpha = new Color(0, 0, 0, 1);
	    _rifle.OnShoot += UpdateRifleAmmo;
	    _shotgun.OnShoot += UpdateShotGunAmmo;
	    _sniper.OnShoot += UpdateSniperAmmo;
	    player.OnPickUp += UpdateMedKitCount;
	    player.OnGunSwitch += UpdateWeaponHUD;
	    UpdateWeaponHUD();
	    if (playerHealth == null) return;
	    player.GetComponent<PlayerHealth>().OnDeath += ActiveDeathMenu;
	    GetComponentInChildren<GrappleCooldown>(true).SetPlayer();
	    GetComponent<HealthManager>().SetPlayer();
    }

    private void OnPlayerReload()
    {
	    aboveHeadUI.SetActive(true);
    }
    
    private readonly List<GameObject> _objectiveMarkers = new ();
    private readonly List<TextMeshProUGUI> _objectiveTexts = new ();

    #region Objectives HUD
    public void UpdateObjectives()
    {
	    foreach (TextMeshProUGUI go in _objectiveTexts)
	    {
		    Destroy(go);
	    }
	    _objectiveTexts.Clear();
	    
	    foreach (GameObject go in _objectiveMarkers)
	    {
		    Destroy(go);
	    }
	    _objectiveMarkers.Clear();
	    
	    int i=0;
	    
	    foreach (ObjectiveStringPair objective in gm.saveData.objectives)
	    {
		    if (objective.isHidden) continue;
		    if (objective.isCompleted) continue;

		    _objectiveMarkers.Add(Instantiate(objectivesMarker, branchProceduralPanel.position, Quaternion.identity));
		    _objectiveMarkers[i].transform.SetParent(branchProceduralPanel.transform, false);
        
		    _objectiveTexts.Add(Instantiate(objectiveText, textProceduralPanel.position, Quaternion.identity));
		    _objectiveTexts[i].transform.SetParent(textProceduralPanel.transform, false);
		    _objectiveTexts[i].text = objective.uiText;
	    }
    }

    #endregion
    
    #region UI Update

    public void OnDisable()
    {
	    if (player == null) return;
        _rifle.OnShoot -= UpdateRifleAmmo;
        _shotgun.OnShoot -= UpdateShotGunAmmo;
        _sniper.OnShoot -= UpdateSniperAmmo;
        player.OnPickUp -= UpdateMedKitCount;
        player.OnGunSwitch -= UpdateWeaponHUD;
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

    public void UpdateMedKitCount()
    {
        medKitText.SetText(player.medkitCount.ToString());
    }

    private void ActiveDeathMenu()
    {
        deathMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(respawnButton);
        respawnButton.GetComponent<Button>().Select();
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

    public IEnumerator StartSaveAnimation()
    {
	    saveIconUIAnimator.speed = 1;
	    
	    // grow
	    saveIconUIAnimator.CrossFade(saveIn.name, 0, 0);
	    string currentStateName = saveIconUIAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
	    yield return new WaitWhile(() => saveIconUIAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentStateName));
	    yield return new WaitForSeconds(saveIconUIAnimator.GetCurrentAnimatorStateInfo(0).length);
	    
	    //spin
	    saveIconUIAnimator.CrossFade(saveSpin.name, 0, 0);
	    currentStateName = saveIconUIAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
	    float spinDelay = Time.time;
	    yield return new WaitWhile(() => saveIconUIAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentStateName));
	    yield return new WaitForSeconds(saveIconUIAnimator.GetCurrentAnimatorStateInfo(0).length);
	    spinDelay = Time.time - spinDelay;
	    
	    //keep spinning while saving
	    while (gm.isSaving)
	    {
		    saveIconUIAnimator.CrossFade(saveSpin.name, 0, 0);
		    currentStateName = saveIconUIAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
		    yield return new WaitForSeconds(spinDelay);
	    }

	    //shrink
	    saveIconUIAnimator.CrossFade(saveOut.name, 0, 0);
	    currentStateName = saveIconUIAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
    }

    public void PlayCollectibleButton(Collectible index)
    {
	    FMODUnity.RuntimeManager.PlayOneShot(audioIndex[(int)index.MyCollectible]);
    }
    
    public void Pause()
    {
	    pauseMenu.SetActive(true);
	    EventSystem.current.SetSelectedGameObject(resumeButton);
	    resumeButton.GetComponent<Button>().Select();
        Cursor.visible = true;
    }
    
    public void Resume()
    {
	    ReturnToMenu();
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }

    public void ResumeButton()
    {
	    GameManager.Instance.Resume();
    }
    
    public void QuitButton()
    {
        Time.timeScale = 1f;
        deathMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gm.QuitGame1();
    }

    public void RespawnButton()
    {
        deathMenu.SetActive(false);
        gm.BeginResetGame();
        Cursor.visible = false;
    }
    
    public void DimButtonGeneralAlpha() => ChangeButtonAlpha(generalOptionsButton.GetComponent<UnityEngine.UI.Button>(), 0.2f);
    
    public void DimButtonAudioAlpha() => ChangeButtonAlpha(audioOptionsButton.GetComponent<UnityEngine.UI.Button>(), 0.2f);
    
    public void DimButtonMonitorAlpha() => ChangeButtonAlpha(monitorOptionsButton.GetComponent<UnityEngine.UI.Button>(), 0.2f);
    
    public void ResetButtonGeneralAlpha() => ChangeButtonAlpha(generalOptionsButton.GetComponent<UnityEngine.UI.Button>(), 1f);
    
    public void ResetButtonAudioAlpha() => ChangeButtonAlpha(audioOptionsButton.GetComponent<UnityEngine.UI.Button>(), 1f);

    public void ResetButtonMonitorAlpha() => ChangeButtonAlpha(monitorOptionsButton.GetComponent<UnityEngine.UI.Button>(), 1f);
    
    private void ChangeButtonAlpha(Button button, float newAlpha)
    {
	    ColorBlock colours = button.colors;
	    colours.normalColor = new Color(colours.normalColor.r, colours.normalColor.g, colours.normalColor.b, newAlpha);
	    button.colors = colours;
    }

    public void ToggleHUD()
    {
	    hUDGameObject.SetActive(gm.saveData.SettingsData.ToggleHUD);
    }

    public void ReturnToMenu()
    {
	    collectibleUI.SetActive(false);
	    controlsUI.SetActive(false);
	    settingsUI.SetActive(false);
	    pauseMenu.SetActive(true);
	    EventSystem.current.firstSelectedGameObject = resumeButton;
	    resumeButton.GetComponent<Selectable>().Select();
    }
}
