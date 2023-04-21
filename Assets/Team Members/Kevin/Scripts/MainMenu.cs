using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public MainMenuNavigator navigator;
	public GameObject gameManagerPrefab;
	private GameManager _gm;
	[SerializeField]
	private CameraFader cameraFader;
	[SerializeField]
	private string newGameSceneName = "Level1_Hangar&Lab";

	[SerializeField]
	private GameObject menuButtons;
	
    public string testFormLink;
    public Animator mmAnimator;

    private string _destination;
    public GameObject newGameButton;
    public GameObject loadButton;

    [HideInInspector]
    public SaveData saveData;
    
    private string _sceneToLoad;
    
    public void Awake()
    {
        mmAnimator.CrossFade("Opening", 0, 0);
        _destination = Path.Combine(Application.persistentDataPath,"saveFile.json");
        if (File.Exists(_destination))
		{
			saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_destination));
			loadButton.SetActive(true);
		}
		else
		{
			Debug.Log("Save File Does Not Exist");
			loadButton.SetActive(false);
		}
        // BinaryFormatter bf = new BinaryFormatter();
        // SaveData data = (SaveData) bf.Deserialize(_file);
        // _file.Close();
    }
    public void StartNewGameButton()
    {
	    if (GameManager.Instance == null)
	    {
		    GameObject go = Instantiate(gameManagerPrefab);
		    _gm = go.GetComponent<GameManager>();
	    }
	    else _gm = GameManager.Instance;
	    _gm.gameLoadedFromFile = false;
	    _sceneToLoad = newGameSceneName;
	    menuButtons.SetActive(false);
	    cameraFader.OnFadeOutComplete += FinishLoadScene;
        cameraFader.FadeOut();
    }
    
    public void LoadSavedGame()
    {
	    if (GameManager.Instance == null)
	    {
		    GameObject go = Instantiate(gameManagerPrefab);
		    _gm = go.GetComponent<GameManager>();
	    }
	    else _gm = GameManager.Instance;
	    _gm.saveData = saveData;
	    _gm.gameLoadedFromFile = true;
	    _sceneToLoad = saveData.sceneName;
	    menuButtons.SetActive(false);
	    cameraFader.OnFadeOutComplete += FinishLoadScene;
	    cameraFader.FadeOut();
    }
    
    private void FinishLoadScene()
    {
	    cameraFader.OnFadeOutComplete -= FinishLoadScene;
	    SceneManager.sceneLoaded += _gm.SetupAfterLevelLoad;
	    SceneManager.LoadScene(_sceneToLoad);
    }

    // public void FeedbackFormButton()
    // {
    //     Application.OpenURL(testFormLink);
    // }
    
    public void ExitGameButton()
    {
        Debug.Log("Game Exited");
        Application.Quit();
    }

    public void OptionsAnimIN()
    {
        mmAnimator.CrossFade("Options", 0, 0);
    }

    public void OptionsAnimOut()
    {
        mmAnimator.CrossFade("Options 0", 0, 0);
    }
}
