using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenuManager : MonoBehaviour
{
    public string testFormLink;
    public Player player;
    private Rifle _rifle;
    private Shotgun _shotgun;
    private Sniper _sniper;

    [SerializeField] private TextMeshProUGUI rifleAmmoText;
    [SerializeField] private TextMeshProUGUI shotgunAmmoText;
    [SerializeField] private TextMeshProUGUI sniperAmmoText;
    public void Awake()
    {
        player = GameManager.Instance.playerPrefab.GetComponent<Player>();
        _rifle = player.GetComponentInChildren<Rifle>();
        _shotgun = player.GetComponentInChildren<Shotgun>();
        _sniper = player.GetComponentInChildren<Sniper>();
        Cursor.visible = true;
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
    

    public void CombatSceneButton()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void MovementTestOneSceneButton()
    {
        SceneManager.LoadScene("Playtest_1_BUILD/Platforming Test A1");
    }

    public void MovementTestTwoSceneButton()
    {
        SceneManager.LoadScene("Playtest_1_BUILD/Platforming Test A2");
    }

    public void FeedbackFormButton()
    {
        Application.OpenURL(testFormLink);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
    
}
