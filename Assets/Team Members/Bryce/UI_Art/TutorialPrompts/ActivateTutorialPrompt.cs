using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTutorialPrompt : MonoBehaviour
{
	public Animator textANIM;

	public float promptDuration;
	
	public GameObject tutorialPrefab;

	public void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();

		if (player != null)
		{
			StartCoroutine(PromptAnim());
		}
	}

	public IEnumerator PromptAnim()
	{
		textANIM.CrossFade("TextFade_IN",0,0);

		yield return new WaitForSeconds(promptDuration);
		
		textANIM.CrossFade("TextFade_OUT",0,0);
	}

	public void disablePrompt()
	{
		tutorialPrefab.SetActive(false);
	}
}
