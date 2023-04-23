using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashTrigger : MonoBehaviour
{
	private ParticleSystem _muzzleFlashParticles;

	private void OnEnable()
	{
		_muzzleFlashParticles = GetComponent<ParticleSystem>();
		GetComponentInParent<WeaponBase>().OnShoot += TriggerParticles;
	}
	
	private void OnDisable()
	{
		GetComponentInParent<WeaponBase>().OnShoot -= TriggerParticles;
	}

	private void TriggerParticles()
	{
		_muzzleFlashParticles.Play();
	}
}
