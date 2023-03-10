using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
   
   //Universal If Paused Bool
   public bool gamePaused;
   
   //Global Reference to player prefab
   public GameObject playerPrefabRef;
   
   //Global Reference to UI Manager
   public UIManager _uiManager;
   
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
}
