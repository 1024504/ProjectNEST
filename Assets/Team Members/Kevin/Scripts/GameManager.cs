using System;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
   
   //Universal Bools
   public bool gamePaused;
   public bool hasShotgun;
   public bool hasSniper;
   public bool canDoubleJump;
   
   //Player Controller
   public PlayerController playerController;
   
   //Global Reference to player prefab
   public GameObject playerPrefabRef;
   
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

   private void Start()
   {
	   foreach (ObjectiveStringPair objective in objectives)
	   {
		   uiManager.UpdateObjective(objective);
	   }
   }

   public void GameReset()
   {
      playerPrefabRef.GetComponent<PlayerHealth>().HealthLevel = 100f;
      gamePaused = false;
      playerPrefabRef.GetComponent<Transform>().position = uiManager.respawnPoint.position;
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
}
