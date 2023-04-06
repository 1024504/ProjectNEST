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

   public bool interactButtonPressed = false;
   
   //Global Reference to player prefab
   public GameObject playerPrefabRef;
   
   //Global Reference to Managers
   public UIManager _uiManager;

   public InteractionEventManager InteractionEventManager;
   
   //Objectives
   public List<String> objectives;
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
      _uiManager = GetComponentInChildren<UIManager>();
   }

   private void Start()
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
