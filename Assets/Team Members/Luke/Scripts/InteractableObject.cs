using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InteractableObject : MonoBehaviour
{
	public Action OnInteract;
	public GameManager.Objectives objective = GameManager.Objectives.None;
	public Action<GameManager.Objectives> OnUpdateObjective;
	public FMODUnity.EventReference interactSFX;
	public bool singleUse;

	protected virtual void Interact() { }

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		Player player = col.GetComponent<Player>();
		if (player == null) return;
		player.OnInteract += Interact;
	}
	
	protected virtual void OnTriggerExit2D(Collider2D col)
	{
		Player player = col.GetComponent<Player>();
		if (player == null) return;
		player.OnInteract -= Interact;
	}
}
