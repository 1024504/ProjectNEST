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

   public void GameReset()
   {
      playerPrefabRef.GetComponent<PlayerHealth>().HealthLevel = 100f;
      gamePaused = false;
      playerPrefabRef.GetComponent<Transform>().position = _uiManager.respawnPoint.position;
      Time.timeScale = 1f;
   }
}
