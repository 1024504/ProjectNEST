using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private static GameManager _instance;

   public bool gamePaused;

   public static GameManager Instance
   {
      get
      {
         if(_instance == null)
            Debug.LogError("Game Manager Null!!!");
         return _instance;
      }
   }
   private void Awake()
   {
      _instance = this; 
   }
}
