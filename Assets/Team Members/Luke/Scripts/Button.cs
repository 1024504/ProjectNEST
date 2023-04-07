using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractableObject
{
	protected override void Interact(Collider2D other)
	{
		OnInteract?.Invoke();
		if (objective != GameManager.Objectives.None)
		{
			GameManager.Instance.UpdateObjective(objective);
		}
	}

	
}
