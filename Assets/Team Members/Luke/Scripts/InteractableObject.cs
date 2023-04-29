using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class InteractableObject : MonoBehaviour
{
	public Action OnInteract;
	public bool singleUse;

	protected virtual void Interact()
	{
		Player.OnInteract -= Interact;
	}

	protected Player Player;

	protected virtual void OnDisable()
	{
		if (Player != null) Player.OnInteract -= Interact;
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		Player = col.GetComponent<Player>();
		if (Player == null) return;
		if (Player.OnInteract != null) Player.OnInteract -= Interact; // Prevents multiple subscriptions (e.g. when player dies and respawns
		Player.OnInteract += Interact;
	}
	
	protected virtual void OnTriggerExit2D(Collider2D col)
	{
		Player = col.GetComponent<Player>();
		if (Player == null) return;
		Player.OnInteract -= Interact;
	}
}
