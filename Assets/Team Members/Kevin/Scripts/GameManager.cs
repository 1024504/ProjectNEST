using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   [Header("Game Tracking")] 
   public float survivalTimer;

   public int killCount;

   public bool gamePaused;
   
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

   public void Update()
   {
      survivalTimer += Time.deltaTime;
   }
   public void KillCountUpdate()
   {
      killCount++;
   }
}
