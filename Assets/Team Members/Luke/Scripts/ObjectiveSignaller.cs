using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSignaller : MonoBehaviour
{
	private GameManager _gm;
	
	private void OnEnable()
	{
		_gm = GameManager.Instance;
	}

	public void SetTrack(GameManager.Objectives objective, bool newCompletedState, bool newHiddenState, bool updateHUD)
	{
		_gm.UpdateObjective(objective, newCompletedState, newHiddenState, updateHUD);
	}
}
