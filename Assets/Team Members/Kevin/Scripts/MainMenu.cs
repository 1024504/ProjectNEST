using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
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
    public GameObject newGamePopup;
    public GameObject newGamePopupNoButton;
    public GameObject loadButton;
    public GameObject generalOptionsButton;
    public GameObject audioOptionsButton;
    public GameObject monitorOptionsButton;

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

    public void ReturnToMainMenuButton()
    {
	    menuButtons.SetActive(true);
	    if (loadButton.activeSelf)
	    {
		    EventSystem.current.firstSelectedGameObject = loadButton;
		    loadButton.GetComponent<Selectable>().Select();
	    }
	    else
	    {
		    EventSystem.current.firstSelectedGameObject = newGameButton;
		    newGameButton.GetComponent<Selectable>().Select();
	    }
    }

    public void StartNewGameButton()
    {
	    if (loadButton.activeSelf)
	    {
		    newGamePopup.SetActive(true);
		    EventSystem.current.firstSelectedGameObject = newGamePopupNoButton;
		    newGamePopupNoButton.GetComponent<Selectable>().Select();
		    menuButtons.SetActive(false);
	    }
	    else StartNewGame();
    }

    public void StartNewGame()
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

    public void DimButtonGeneralAlpha() => ChangeButtonAlpha(generalOptionsButton.GetComponent<UnityEngine.UI.Button>(), 0.2f);
    
    public void DimButtonAudioAlpha() => ChangeButtonAlpha(audioOptionsButton.GetComponent<UnityEngine.UI.Button>(), 0.2f);
    
    public void DimButtonMonitorAlpha() => ChangeButtonAlpha(monitorOptionsButton.GetComponent<UnityEngine.UI.Button>(), 0.2f);
    
    public void ResetButtonGeneralAlpha() => ChangeButtonAlpha(generalOptionsButton.GetComponent<UnityEngine.UI.Button>(), 1f);
    
	public void ResetButtonAudioAlpha() => ChangeButtonAlpha(audioOptionsButton.GetComponent<UnityEngine.UI.Button>(), 1f);

	public void ResetButtonMonitorAlpha() => ChangeButtonAlpha(monitorOptionsButton.GetComponent<UnityEngine.UI.Button>(), 1f);
    
    private void ChangeButtonAlpha(UnityEngine.UI.Button button, float newAlpha)
    {
	    ColorBlock colours = button.colors;
	    colours.normalColor = new Color(colours.normalColor.r, colours.normalColor.g, colours.normalColor.b, newAlpha);
	    button.colors = colours;
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
