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

	[HideInInspector]
	public Player player;
	
	public int globalDoorId;

	private CameraTracker _cameraTracker;

	public FMODUnity.EventReference doorSFX;

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

	private void OnDisable()
	{
		LevelManager.Instance.doors.Remove(this);
		if (connectingDoor != null) connectingDoor.connectingDoor = null;
	}

	protected override void Interact()
	{
		if (sceneToLoad == null)
		{
			Debug.Log("No scene to load");
			return;
		}

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
		Vector3 targetPosition = connectingDoor.transform.position;
		player.gameObject.transform.position = targetPosition;
		_cameraTracker.transform.position = targetPosition+Vector3.back;
		connectingDoor.player = player;
		LevelManager.Instance.OnSceneUnloaded += LoadStage3;
		LevelManager.Instance.UnloadScene(connectingDoor.sceneToLoad);
	}
	
	private void LoadStage3()
	{
		LevelManager.Instance.OnSceneUnloaded -= LoadStage3;
		_cameraTracker.cameraFader.OnFadeInComplete += LoadStage4;
		_cameraTracker.cameraFader.FadeIn();
	}
	
	private void LoadStage4()
	{
		_cameraTracker.cameraFader.OnFadeInComplete -= LoadStage4;
		GameManager.Instance.EnableInput();
	}

	protected override void OnTriggerEnter2D(Collider2D col)
	{
		base.OnTriggerEnter2D(col);
		player = col.GetComponent<Player>();
	}
    
	protected override void OnTriggerExit2D(Collider2D col)
	{
		base.OnTriggerExit2D(col);
		player = null;
	}
}

