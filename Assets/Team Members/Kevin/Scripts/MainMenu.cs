using System.Collections.Generic;
using System.IO;
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
	    if (GameManager.Instance == null) Instantiate(gameManagerPrefab);
	    _gm = GameManager.Instance;
	    _gm.playerControls = new();
	    mmAnimator.CrossFade("Opening", 0, 0);
	    _destination = Path.Combine(Application.persistentDataPath,"saveFile.json");
	    if (File.Exists(_destination))
	    {
		    saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_destination));
		    _gm.saveData = saveData;
		    loadButton.SetActive(true);
		    EventSystem.current.firstSelectedGameObject = loadButton;
		    loadButton.GetComponent<Selectable>().Select();
		    
		    _gm.ApplySettings();
	    }
	    else
	    {
		    Debug.Log("Save File Does Not Exist");
		    loadButton.SetActive(false);
		    EventSystem.current.firstSelectedGameObject = newGameButton;
		    newGameButton.GetComponent<Selectable>().Select();
		    
		    Resolution currentResolution = Screen.currentResolution;
		    bool fullscreen = Screen.fullScreen;
		    int quality = QualitySettings.GetQualityLevel();
		    float masterVolume = 1;
		    float musicVolume = 1;
		    float sfxVolume = 1;
		    List<ObjectiveStringPair> defaultObjectives = new List<ObjectiveStringPair>() 
		    {
			    new (GameManager.Objectives.EscapeHangar, "Open the hangar door", false, false), 
			    new (GameManager.Objectives.TurnOnGenerator, "Turn on power", false, true), 
			    new (GameManager.Objectives.ExploreLab, "Investigate the lab", false, true), 
			    new (GameManager.Objectives.EnterPlaza, "Enter the plaza", false, true), 
			    new (GameManager.Objectives.FindHawk, "Find Hawk", false, true), 
			    new (GameManager.Objectives.FindRaven, "Find Raven", false, true), 
			    new (GameManager.Objectives.FindEagle, "Find Eagle", false, true), 
			    new (GameManager.Objectives.DefeatBoss, "Defeat the Warrior", false, true)
			    
		    };
		    
		    // Default Settings
		    // Update this when new settings are added
		    _gm.saveData = new SaveData
		    {
			    objectives =defaultObjectives,
			    SettingsData = new SettingsData("", true, 0.05f, true, true, masterVolume, musicVolume, sfxVolume, currentResolution, fullscreen, quality)
		    };
	    }
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
	    _gm.gameLoadedFromFile = false;
	    _sceneToLoad = newGameSceneName;
	    menuButtons.SetActive(false);
	    cameraFader.OnFadeOutComplete += FinishLoadScene;
        cameraFader.FadeOut();
    }
    
    public void LoadSavedGame()
    {
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

    public void DimButtonGeneralAlpha() => ChangeButtonAlpha(generalOptionsButton.GetComponent<Button>(), 0.2f);
    
    public void DimButtonAudioAlpha() => ChangeButtonAlpha(audioOptionsButton.GetComponent<Button>(), 0.2f);
    
    public void DimButtonMonitorAlpha() => ChangeButtonAlpha(monitorOptionsButton.GetComponent<Button>(), 0.2f);
    
    public void ResetButtonGeneralAlpha() => ChangeButtonAlpha(generalOptionsButton.GetComponent<Button>(), 1f);
    
	public void ResetButtonAudioAlpha() => ChangeButtonAlpha(audioOptionsButton.GetComponent<Button>(), 1f);

	public void ResetButtonMonitorAlpha() => ChangeButtonAlpha(monitorOptionsButton.GetComponent<Button>(), 1f);
    
    private void ChangeButtonAlpha(Button button, float newAlpha)
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
