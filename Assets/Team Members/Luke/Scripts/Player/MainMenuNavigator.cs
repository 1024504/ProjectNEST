using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuNavigator : MonoBehaviour, IUiControllable
{
	public MainMenu mainMenu;

	public void NavigatePerformed(Vector2 input)
	{
		UnityEngine.UI.Selectable currentSelectable = EventSystem.current.currentSelectedGameObject.GetComponent<UnityEngine.UI.Selectable>();
		if (currentSelectable == null)
		{
			currentSelectable = EventSystem.current.firstSelectedGameObject.GetComponent<UnityEngine.UI.Selectable>();
		}
		
		UnityEngine.UI.Selectable nextSelectable = currentSelectable.FindSelectable(input);
		if (nextSelectable == null) return;
		EventSystem.current.SetSelectedGameObject(nextSelectable.gameObject);
	}

	public void NavigateCancelled()
	{
		
	}

	public void PointPerformed(Vector2 input)
	{
		
	}

	public void PointCancelled()
	{
		
	}

	public void ClickPerformed()
	{
		
	}

	public void ClickCancelled()
	{
		
	}

	public void ReturnPerformed()
	{
		
	}

	public void ReturnCancelled()
	{
		
	}

	public void ResumePerformed()
	{
		
	}

	public void ResumeCancelled()
	{
		
	}
}
