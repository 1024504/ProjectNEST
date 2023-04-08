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
   
   //Universal Bools
   public bool gamePaused;
   
   //Player Controller
   public PlayerController playerController;
   
   //Global Reference to player prefab
   public GameObject playerPrefab;
   public Vector3 defaultSpawnPoint;
   public GameObject cameraPrefab;

   [HideInInspector]
   public SaveData saveData;

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
      uiManager = GetComponentInChildren<UIManager>();
   }

   public void SetupAfterLevelLoad(Scene scene, LoadSceneMode mode)
   {
	   SceneManager.sceneLoaded -= SetupAfterLevelLoad;
	   if (gameLoadedFromFile)
	   {
		   GameObject go = Instantiate(playerPrefab, saveData.playerPosition, Quaternion.identity);
		   Player player = go.GetComponent<Player>();
		   player.GetComponent<PlayerHealth>().HealthLevel = saveData.playerHealth;
		   player.doubleJumpEnabled = saveData.canDoubleJump;
		   player.grappleEnabled = saveData.canGrapple;
		   player.medkitCount = saveData.totalMedkits;
		   objectives = saveData.objectives;
		   go = Instantiate(cameraPrefab, saveData.playerPosition+Vector3.back, Quaternion.identity);
		   CameraTracker cameraTracker = go.GetComponent<CameraTracker>();
		   cameraTracker.playerTransform = player.transform;
		   uiManager.aboveHeadUI = player.aboveHeadUI;
	   }
	   else
	   {
		   GameObject go = Instantiate(playerPrefab, defaultSpawnPoint, Quaternion.identity);
		   Player player = go.GetComponent<Player>();
		   go = Instantiate(cameraPrefab, defaultSpawnPoint+Vector3.back, Quaternion.identity);
		   CameraTracker cameraTracker = go.GetComponent<CameraTracker>();
		   cameraTracker.playerTransform = player.transform;
	   }
	   foreach (ObjectiveStringPair objective in objectives)
	   {
		   uiManager.UpdateObjective(objective);
	   }
   }

   public void GameReset()
   {
      playerPrefab.GetComponent<PlayerHealth>().HealthLevel = 100f;
      gamePaused = false;
      playerPrefab.GetComponent<Transform>().position = uiManager.respawnPoint.position;
      Time.timeScale = 1f;
   }

   public void DisableInput() => playerController.Controls.Player.Disable();
   
   public void EnableInput() => playerController.Controls.Player.Enable();

   public void Pause()
   {
	   gamePaused = true;
	   DisableInput();
	   playerController.Controls.UI.Enable();
	   Time.timeScale = 0f;
	   uiManager.Pause();
   }
   
   public void Resume()
   {
	   gamePaused = false;
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
	   string destination = Path.Combine(Application.persistentDataPath,"saveFile.json");

	   Player player = (Player) playerController.Agent;
	   
	   saveData = new
	   (
		   UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
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
	   
	   Debug.Log("Saved on the GM!");
   }
}
