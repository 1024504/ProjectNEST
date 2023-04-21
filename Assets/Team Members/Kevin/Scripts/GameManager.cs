using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   public bool gameLoadedFromFile;

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
	   None,
	   EscapeHangar,
	   TurnOnGenerator,
	   ExploreLab
   }
   
   // List of objective strings for UI, fill out in inspector
   public List<ObjectiveStringPair> objectives = new();
   
   //Objectives
   // public List<String> objectives;
   public int currentMission;
   
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         //Debug.Log("GameManager == Null!");
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
      uiManager = GetComponentInChildren<UIManager>(true);
   }

   public void SetupAfterLevelLoad(Scene scene, LoadSceneMode mode)
   {
	   uiManager.gameObject.SetActive(true);
	   LevelManager.Instance.InstantiateDestroyOnLoad();

	   SceneManager.sceneLoaded -= SetupAfterLevelLoad;
	   if (gameLoadedFromFile)
	   {
		   GameObject go = Instantiate(playerPrefab, saveData.playerPosition, Quaternion.identity);
		   _player = go.GetComponent<Player>();
		   _player.GetComponent<PlayerHealth>().HealthLevel = saveData.playerHealth;
		   _player.doubleJumpEnabled = saveData.canDoubleJump;
		   _player.grappleEnabled = saveData.canGrapple;
		   _player.medkitCount = saveData.totalMedkits;
		   objectives = saveData.objectives;
		   go = Instantiate(cameraPrefab, saveData.playerPosition+Vector3.back, Quaternion.identity);
		   cameraTracker = go.GetComponent<CameraTracker>();
	   }
	   else
	   {
		   GameObject go = Instantiate(playerPrefab, defaultSpawnPoint, Quaternion.identity);
		   _player = go.GetComponent<Player>();
		   go = Instantiate(cameraPrefab, defaultSpawnPoint+Vector3.back, Quaternion.identity);
		   cameraTracker = go.GetComponent<CameraTracker>();
	   }
	   
	   cameraTracker.playerTransform = _player.transform;
	   uiManager.aboveHeadUI = _player.aboveHeadUI;
	   
	   foreach (ObjectiveStringPair objective in objectives)
	   {
		   uiManager.UpdateObjective(objective);
	   }
	   
	   cameraTracker.cameraFader.FadeIn();
	   cameraTracker.cameraFader.OnFadeInComplete += ResumeOnLoad;
   }

   private void ResumeOnLoad()
   {
	   cameraTracker.cameraFader.OnFadeInComplete -= ResumeOnLoad;
	   Resume();
   }

   public void BeginQuitGame()
   {
	   uiManager.gameObject.SetActive(false);
	   cameraTracker.cameraFader.OnFadeOutComplete += CompleteQuitGame;
	   cameraTracker.cameraFader.FadeOut();
   }

   private void CompleteQuitGame()
   {
	   cameraTracker.cameraFader.OnFadeOutComplete -= CompleteQuitGame;
	   Destroy(_player.gameObject);
	   cameraTracker.transform.parent = LevelManager.Instance.destroyOnLoad.transform;
	   _player = null;
	   cameraTracker = null;
	   SceneManager.LoadScene("MainMenu");
   }

   public void GameReset()
   {
	   PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
	   playerHealth.HealthLevel += playerHealth.maxHealth; 
	   if (saveData != null) _player.transform.position = saveData.playerPosition;
	   else _player.transform.position = defaultSpawnPoint;
	   Resume();
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

   public void UpdateObjective(Objectives objective)
   {
	   ObjectiveStringPair objectiveToUpdate = new ();
	   foreach (ObjectiveStringPair objectiveStringPair in objectives)
	   {
		   if (objectiveStringPair.objective != objective) continue;
		   objectiveToUpdate = objectiveStringPair;
		   objectiveStringPair.isCompleted = true;
		   break;
	   }
	   if (objectiveToUpdate.objective == Objectives.None) return;
	   uiManager.UpdateObjective(objectiveToUpdate);
   }

   public void SaveCheckpoint(Checkpoint checkpoint)
   {
	   isSaving = true;
	   StartCoroutine(uiManager.StartSaveAnimation());
	   FMODUnity.RuntimeManager.PlayOneShot(savedTriggered);
	   
	   string destination = Path.Combine(Application.persistentDataPath,"saveFile.json");

	   Player player = (Player) playerController.GameplayAgent;
	   
	   saveData = new
	   (
		   SceneManager.GetActiveScene().name,
		   checkpoint.transform.position,
		   player.GetComponent<PlayerHealth>().HealthLevel,
		   true,
		   true,
		   player.doubleJumpEnabled,
		   player.grappleEnabled,
		   player.medkitCount,
		   objectives
	   );
	   
	   string json = JsonUtility.ToJson(saveData);
	   File.WriteAllText(destination, json);
	   
	   isSaving = false;
   }
}
