using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashTrigger : MonoBehaviour
{
	private ParticleSystem _muzzleFlashParticles;
	private WeaponBase _weaponBase;
	
	private void OnEnable()
	{
		_muzzleFlashParticles = GetComponent<ParticleSystem>();
		_weaponBase = GetComponentInParent<WeaponBase>();
		if (_weaponBase != null) GetComponentInParent<WeaponBase>().OnShoot += TriggerParticles;
	}
	
	private void OnDisable()
	{
		
		if (_weaponBase != null) _weaponBase.OnShoot -= TriggerParticles;
	}

	private void TriggerParticles()
	{
		_muzzleFlashParticles.Play();
	}
}
