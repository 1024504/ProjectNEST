using System;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
   
   //Universal Bools
   public bool gamePaused;
   public bool hasShotgun;
   public bool hasSniper;
   public bool canDoubleJump;

   public bool interactButtonPressed = false;
   
   //Global Reference to player prefab
   public GameObject playerPrefabRef;
   
   //Global Reference to Managers
   public UIManager _uiManager;

   public InteractionEventManager InteractionEventManager;
   
   //Objectives
   public List<String> objectives;
   public int currentMission;

   /*public enum TutorialObjectives
   {
      Hangar,
      Generator,
      Lab
   }*/

   //public TutorialObjectives theObjective;
   public void Update()
   {
      /*switch (theObjective)
      {
         case TutorialObjectives.Hangar:
            currentMission = 0;
            _uiManager.UpdateObjectives();
            break;
         case TutorialObjectives.Generator:
            currentMission = 1;
            _uiManager.UpdateObjectives();
            break;
         case TutorialObjectives.Lab:
            currentMission = 2;
            _uiManager.UpdateObjectives();
            break;
      }*/
   }


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
   }

   private void OnEnable()
   {
      _uiManager.UpdateObjectives();     
   }

   public void GameReset()
   {
      playerPrefabRef.GetComponent<PlayerHealth>().HealthLevel = 100f;
      gamePaused = false;
      playerPrefabRef.GetComponent<Transform>().position = _uiManager.respawnPoint.position;
      Time.timeScale = 1f;
   }
}
