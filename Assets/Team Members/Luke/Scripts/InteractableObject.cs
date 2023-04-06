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
	
	private void OnTriggerStay2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player == null || !player.interactButtonPressed) return;
		FMODUnity.RuntimeManager.PlayOneShot(interactSFX);
		Interact();
		if (singleUse) gameObject.SetActive(false);
	}
}
