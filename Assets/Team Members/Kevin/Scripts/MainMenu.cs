using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject gameManagerPrefab;
	private GameManager _gm;
	
    public string testFormLink;
    public Animator mmAnimator;

    private string _destination;
    public GameObject loadButton;
    
    public SaveData saveData;
    
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
	    GameObject go = Instantiate(gameManagerPrefab);
        _gm = go.GetComponent<GameManager>();
        _gm.gameLoadedFromFile = false;
        SceneManager.sceneLoaded += _gm.SetupAfterLevelLoad;
        SceneManager.LoadScene("GreyboxLevel1Tutorial");
    }

    public void LoadSavedGame()
    {
	    GameObject go = Instantiate(gameManagerPrefab);
	    _gm = go.GetComponent<GameManager>();
	    _gm.saveData = saveData;
	    _gm.gameLoadedFromFile = true;
	    SceneManager.sceneLoaded += _gm.SetupAfterLevelLoad;
	    SceneManager.LoadScene(saveData.sceneName);
    }

    public void FeedbackFormButton()
    {
        Application.OpenURL(testFormLink);
    }
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
