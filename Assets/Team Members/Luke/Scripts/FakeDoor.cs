using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoor : MonoBehaviour
{
	public GameObject fakeDoor;
	public Collider2D realDoor;
	public Collider2D buttonCollider;

	private void OnEnable()
	{
		GameManager.Instance.OnFinishLoading += CheckObjective;
		LevelManager.Instance.OnSceneLoaded += CheckObjective;
	}

	private void OnDisable()
	{
		GameManager.Instance.OnFinishLoading -= CheckObjective;
		LevelManager.Instance.OnSceneLoaded -= CheckObjective;
	}

	private void CheckObjective()
	{
		if (!GameManager.Instance.saveData.objectives[2].isCompleted) return;
		fakeDoor.SetActive(false);
		realDoor.enabled = true;
		buttonCollider.enabled = false;
	}
}
