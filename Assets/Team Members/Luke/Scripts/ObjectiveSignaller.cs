using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSignaller : MonoBehaviour
{
	private GameManager _gm;
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(2f);
		_gm = GameManager.Instance;
	}

	public void SetObjectiveComplete(int objectiveIndex)
	{
		foreach (ObjectiveStringPair objective in _gm.saveData.objectives)
		{
			if (objective.objective != (GameManager.Objectives) objectiveIndex) continue;
			objective.isCompleted = true;
			break;
		}
	}
	
	public void ToggleObjectiveHidden(int objectiveIndex)
	{
		foreach (ObjectiveStringPair objective in _gm.saveData.objectives)
		{
			if (objective.objective != (GameManager.Objectives) objectiveIndex) continue;
			objective.isHidden = !objective.isHidden;
			break;
		}
	}

	public void UpdateHUD()
	{
		_gm.uiManager.UpdateObjectives();
	}
}
