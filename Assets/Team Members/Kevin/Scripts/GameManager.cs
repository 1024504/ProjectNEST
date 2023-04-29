using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   public bool gameLoadedFromFile;
   public PlayerControls playerControls;

   //Player Controller
   public PlayerController playerController;
   private Player _player;
   public CameraTracker cameraTracker;

   //Global Reference to player prefab
   public GameObject playerPrefab;
   public Vector3 defaultSpawnPoint;
   public GameObject cameraPrefab;

   [HideInInspector]
   public SaveData saveData;
   public FMODUnity.EventReference savedTriggered;
   public bool isSaving;

   //Global Reference to Managers
   public UIManager uiManager;

   [Serializable]
   public enum Objectives
   {
	   None=0,
	   EscapeHangar=1,
	   TurnOnGenerator=2,
	   ExploreLab=3,
	   EnterPlaza=4,
	   PlazaLeft=5,
	   PlazaRight=6,
	   FindHawk=7,
	   FindRaven=8,
	   FindEagle=9,
	   DefeatBoss=10
   }

   private Settings _settings;

   public Action OnFinishLoading;
   
   //Objectives
   // public List<String> objectives;
   public int currentMission;
   
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
      uiManager = GetComponentInChildren<UIManager>();
      _settings = GetComponentInChildren<Settings>();
   }

   private void Start()
   {
	   GetComponentInChildren<MusicManagerScript>().TrackSelector = 6;
   }

   public void SetupAfterLevelLoad(Scene scene, LoadSceneMode mode)
   {
	   uiManager.hUDGameObject.SetActive(true);
	   LevelManager.Instance.InstantiateDestroyOnLoad();

	   SceneManager.sceneLoaded -= SetupAfterLevelLoad;
	   if (gameLoadedFromFile)
	   {
		   GameObject go = Instantiate(playerPrefab, saveData.playerPosition, Quaternion.identity);
		   _player = go.GetComponent<Player>();
		   _player.hasShotgun = saveData.hasShotgun;
		   if (_player.hasShotgun) uiManager.TurnOnShotgunHUD();
		   _player.hasSniper = saveData.hasSniper;
		   if (_player.hasSniper) uiManager.TurnOnSniperHUD();
		   _player.doubleJumpEnabled = saveData.canDoubleJump;
		   _player.grappleEnabled = saveData.canGrapple;
		   if (_player.grappleEnabled) uiManager.TurnOnGrappleHUD();
		   _player.medkitCount = saveData.totalMedkits;
		   uiManager.hUDGameObject.SetActive(saveData.SettingsData.ToggleHUD);
		   uiManager.UpdateMedKitCount();
		   _player.GetComponent<CollectiblesBag>().hasCollectibles = saveData.collectibles;
		   ApplySettings();
		   go = Instantiate(cameraPrefab, saveData.playerPosition+Vector3.back, Quaternion.identity);
		   cameraTracker = go.GetComponent<CameraTracker>();
		   GetComponentInChildren<MusicManagerScript>().TrackSelector = saveData.currentMusicTrack;
	   }
	   else
	   {
		   GameObject go = Instantiate(playerPrefab, defaultSpawnPoint, Quaternion.identity);
		   _player = go.GetComponent<Player>();
		   go = Instantiate(cameraPrefab, defaultSpawnPoint+Vector3.back, Quaternion.identity);
		   cameraTracker = go.GetComponent<CameraTracker>();
		   GetComponentInChildren<MusicManagerScript>().TrackSelector = 0;
	   }
	   OnFinishLoading?.Invoke();
	   
	   cameraTracker.playerTransform = _player.transform;
	   uiManager.aboveHeadUI = _player.aboveHeadUI;
	   
	   uiManager.UpdateObjectives();
	   
	   cameraTracker.cameraFader.FadeIn();
	   cameraTracker.cameraFader.OnFadeInComplete += ResumeOnLoad;
   }

   private void ResumeOnLoad()
   {
	   cameraTracker.cameraFader.OnFadeInComplete -= ResumeOnLoad;
	   Resume();
   }

   public void QuitGame1()
   {
	   uiManager.hUDGameObject.SetActive(false);
	   cameraTracker.cameraFader.OnFadeOutComplete += QuitGame2;
	   cameraTracker.cameraFader.FadeOut();
   }

   private void QuitGame2()
   {
	   cameraTracker.cameraFader.OnFadeOutComplete -= QuitGame2;
	   Destroy(_player.gameObject);
	   cameraTracker.transform.parent = LevelManager.Instance.destroyOnLoad.transform;
	   _player = null;
	   cameraTracker = null;
	   
	   LevelManager.Instance.OnSceneLoaded += QuitGame3;
	   LevelManager.Instance.LoadScene("MainMenu");
   }
   
   private void QuitGame3()
   {
	   LevelManager.Instance.OnSceneLoaded -= QuitGame3;
	   string sceneToUnload = SceneManager.GetActiveScene().name;
	   LevelManager.Instance.SetActiveScene("MainMenu");
	   GetComponentInChildren<MusicManagerScript>().TrackSelector = 6;
	   LevelManager.Instance.UnloadScene(sceneToUnload);
   }
   
   public void FinishGame1()
   {
	   uiManager.hUDGameObject.SetActive(false);
	   cameraTracker.cameraFader.OnFadeOutComplete += QuitGame2;
	   cameraTracker.cameraFader.FadeOut();
   }

   private void FinishGame2()
   {
	   cameraTracker.cameraFader.OnFadeOutComplete -= QuitGame2;
	   Destroy(_player.gameObject);
	   cameraTracker.transform.parent = LevelManager.Instance.destroyOnLoad.transform;
	   _player = null;
	   cameraTracker = null;
	   
	   LevelManager.Instance.OnSceneLoaded += QuitGame3;
	   LevelManager.Instance.LoadScene("Closing_Cutscene");
   }
   
   private void FinishGame3()
   {
	   LevelManager.Instance.OnSceneLoaded -= QuitGame3;
	   string sceneToUnload = SceneManager.GetActiveScene().name;
	   LevelManager.Instance.SetActiveScene("Closing_Cutscene");
	   LevelManager.Instance.UnloadScene(sceneToUnload);
   }
   
   public void BeginResetGame()
   {
	   Time.timeScale = 1f;
	   uiManager.hUDGameObject.SetActive(false);
	   cameraTracker.cameraFader.OnFadeOutComplete += CompleteResetGame;
	   cameraTracker.cameraFader.FadeOut();
   }
   
   private void CompleteResetGame()
   {
	   cameraTracker.cameraFader.OnFadeOutComplete -= CompleteResetGame;
	   cameraTracker.transform.parent = LevelManager.Instance.destroyOnLoad.transform;
	   _player = null;
	   cameraTracker = null;
	   if (saveData != null) SceneManager.LoadScene(saveData.sceneName);
	   else SceneManager.LoadScene("Level1_Hangar&Lab");
	   SceneManager.sceneLoaded += SetupAfterLevelLoad;
   }

   public void DisableInput() => playerController.Controls.Player.Disable();
   
   public void EnableInput() => playerController.Controls.Player.Enable();

   public void Pause()
   {
	   DisableInput();
	   playerController.Controls.UI.Enable();
	   Time.timeScale = 0f;
	   uiManager.Pause();
   }

   public void Resume()
   {
	   EnableInput();
	   playerController.Controls.UI.Disable();
	   Time.timeScale = 1f;
	   uiManager.Resume();
   }
   
   public void ApplySettings()
   {
	   playerControls.LoadBindingOverridesFromJson(saveData.SettingsData.ControlsOverrides);
	   _settings.ApplyChanges();
   }

   public void SaveGame()
   {
	   isSaving = true;
	   StartCoroutine(uiManager.StartSaveAnimation());
	   FMODUnity.RuntimeManager.PlayOneShot(savedTriggered);

	   string destination = Path.Combine(Application.persistentDataPath, "saveFile.json");
	   string json = JsonUtility.ToJson(saveData);
	   File.WriteAllText(destination, json);
	   
	   isSaving = false;
   }
}
