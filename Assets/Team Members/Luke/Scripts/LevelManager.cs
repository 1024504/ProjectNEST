using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance;
	
	public List<TransLevelDoor> doors;

	public Action OnSceneLoaded;
	public Action OnSceneUnloaded;
	public GameObject destroyOnLoad;

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
		InstantiateDestroyOnLoad();
	}
	
	public void InstantiateDestroyOnLoad()
	{
		destroyOnLoad = Instantiate(new GameObject());
		destroyOnLoad.name = "DestroyOnLoad";
	}

	public void LoadScene(string sceneToLoad)
	{
		SceneManager.sceneLoaded += OnSceneLoadedCallback;
		SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
	}
	
	private void OnSceneLoadedCallback(Scene scene, LoadSceneMode mode)
	{
		StartCoroutine(LoadFrameDelay());
	}
	
	private IEnumerator LoadFrameDelay()
	{
		for (int i = 0; i < 5; i++)
		{
			yield return new WaitForEndOfFrame();
		}
		InstantiateDestroyOnLoad();
		OnSceneLoaded?.Invoke();
	}

	public void SetActiveScene(string sceneToActivate) => SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToActivate));

	public void UnloadScene(string sceneToUnload)
	{
		SceneManager.sceneUnloaded += OnSceneUnloadedCallback;
		SceneManager.UnloadSceneAsync(sceneToUnload);
	}

	private void OnSceneUnloadedCallback(Scene scene)
	{
		StartCoroutine(UnloadFrameDelay());
	}
	
	private IEnumerator UnloadFrameDelay()
	{
		for (int i = 0; i < 5; i++)
		{
			yield return new WaitForEndOfFrame();
		}
		InstantiateDestroyOnLoad();
		OnSceneUnloaded?.Invoke();
	}
	
	public TransLevelDoor GetLinkedDoor(TransLevelDoor door)
	{
		foreach (TransLevelDoor levelDoors in doors)
		{
			if (levelDoors == door) continue;
			if (door.globalDoorId == levelDoors.globalDoorId) return levelDoors;
		}
		
		return null;
	}
}