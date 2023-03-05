using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   [Header("Game Tracking")] 
   public float survivalTimer;

   public int winCondition;

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
   public void KillCountUpdate()
   {
      killCount++;
      /*if (killCount == winCondition)
      {
         //win game take to main menu
      }*/
   }
}
