using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransLevelDoor : InteractableObject
{
	public string sceneToLoad;
	
	public TransLevelDoor connectingDoor;

	public int globalDoorId;

	private CameraTracker _cameraTracker;

	public FMODUnity.EventReference doorSFX;

	private Scene _myScene;

	private void OnEnable()
	{
		if (LevelManager.Instance == null) return;
		connectingDoor = LevelManager.Instance.GetLinkedDoor(this);
		if (connectingDoor != null) connectingDoor.connectingDoor = this;
	}

	private void Start()
	{
		LevelManager.Instance.doors.Add(this);
	}

	protected override void OnDisable()
	{
		LevelManager.Instance.doors.Remove(this);
		if (connectingDoor != null) connectingDoor.connectingDoor = null;
		base.OnDisable();
	}

	protected override void Interact()
	{
		base.Interact();
		if (sceneToLoad == null)
		{
			Debug.Log("No scene to load");
			return;
		}
		
		_myScene = SceneManager.GetActiveScene();
		
		FMODUnity.RuntimeManager.PlayOneShot(doorSFX);
		
		GameManager.Instance.DisableInput();
		_cameraTracker = GameManager.Instance.cameraTracker;
		_cameraTracker.cameraFader.OnFadeOutComplete += LoadStage1;
		_cameraTracker.cameraFader.FadeOut();
	}

	private void LoadStage1()
	{
		_cameraTracker.cameraFader.OnFadeOutComplete -= LoadStage1;
		LevelManager.Instance.OnSceneLoaded += LoadStage2;
		LevelManager.Instance.LoadScene(sceneToLoad);
	}

	private void LoadStage2()
	{
		LevelManager.Instance.OnSceneLoaded -= LoadStage2;
		LevelManager.Instance.SetActiveScene(sceneToLoad);
		StartCoroutine(FrameDelay());
	}

	private IEnumerator FrameDelay()
	{
		for (int i = 0; i < 10; i++)
		{
			yield return new WaitForEndOfFrame();
		}
		LoadStage3();
	}

	private void LoadStage3()
	{
		Vector3 targetPosition = connectingDoor.transform.position;
		GameManager.Instance.playerController.GameplayAgent.gameObject.transform.position = targetPosition;
		_cameraTracker.transform.position = targetPosition+Vector3.back;
		LevelManager.Instance.OnSceneUnloaded += LoadStage4;
		LevelManager.Instance.UnloadScene(_myScene.name);
	}
	
	private void LoadStage4()
	{
		LevelManager.Instance.OnSceneUnloaded -= LoadStage4;
		_cameraTracker.cameraFader.OnFadeInComplete += LoadStage5;
		_cameraTracker.cameraFader.FadeIn();
	}
	
	private void LoadStage5()
	{
		_cameraTracker.cameraFader.OnFadeInComplete -= LoadStage5;
		GameManager.Instance.EnableInput();
	}
}

