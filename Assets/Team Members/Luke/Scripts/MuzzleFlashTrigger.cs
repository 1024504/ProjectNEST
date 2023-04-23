using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashTrigger : MonoBehaviour
{
	[SerializeField] private ParticleSystem muzzleFlashParticles;

	private void OnEnable()
	{
		GetComponentInParent<WeaponBase>().OnShoot += TriggerParticles;
	}
	
	private void OnDisable()
	{
		GetComponentInParent<WeaponBase>().OnShoot -= TriggerParticles;
	}

	private void TriggerParticles()
	{
		muzzleFlashParticles.Play();
	}
}
