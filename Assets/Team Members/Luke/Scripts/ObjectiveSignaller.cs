using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSignaller : MonoBehaviour
{
	public GameManager _gm;
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(2f);
		_gm = GameManager.Instance;
	}

	public void SetObjectiveString(string objectiveString)
	{
		_gm.setObjectiveString = objectiveString;
	}
	public void SetCompleteState(bool newCompleteState)
	{
		_gm.setNewCompletedState = newCompleteState;
	}

	public void SetNewHiddenState(bool newHiddenState)
	{
		_gm.setNewHiddenState = newHiddenState;
	}

	public void SetUpdateHUD(bool updateHUD)
	{
		_gm.UpdateHUD(updateHUD);
	}

	/*public void SetTrack(GameManager.Objectives objective, bool newCompletedState, bool newHiddenState, bool updateHUD)
	{
		_gm.UpdateObjective(objective, newCompletedState, newHiddenState, updateHUD);
	}
	*/
}
