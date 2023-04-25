using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Random = UnityEngine.Random;

public class Sparks_SFX_Script : MonoBehaviour
{
	public GameObject sparkSFX;
	public ParticleSystem sparks;

	public void OnEnable()
	{
		SparkSound();
	}

	public void SparkSound()
	{
		StartCoroutine(SparkSoundDelay());
	}

	public IEnumerator SparkSoundDelay()
	{
		sparkSFX.GetComponent<FMODUnity.StudioEventEmitter>().Play();
		yield return new WaitForSeconds(Random.Range(1f,2f));
		SparkSound();
	}
}


