using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class BossAnimationManager : AnimationManagerBase
{
	public AnimationClip shootBuildUp;
	public AnimationClip shoot;
	public AnimationClip melee;
	public AnimationClip walk;
	public AnimationClip run;
	public AnimationClip prepareToRun;

	private AlveriumBoss _boss;

	public GameObject movementSFX;
	public GameObject screechSFX;
	public EventReference meleeSwishSFX;
	public GameObject shootSFX;

	protected override void OnEnable()
	{
		base.OnEnable();
		_boss = GetComponentInParent<AlveriumBoss>();
		_boss.OnShootBuildup += ShootBuildUpAnimation;
		_boss.OnShoot += ShootAnimation;
		_boss.OnMelee += MeleeAnimation;
		_boss.OnWalk += WalkAnimation;
		_boss.OnRun += RunAnimation;
		_boss.OnPrepareToRun += PrepareToRunAnimation;
	}
	
	private void OnDisable()
	{
		_boss.OnShootBuildup -= ShootBuildUpAnimation;
		_boss.OnShoot -= ShootAnimation;
		_boss.OnMelee -= MeleeAnimation;
		_boss.OnWalk -= WalkAnimation;
		_boss.OnRun -= RunAnimation;
		_boss.OnPrepareToRun -= PrepareToRunAnimation;
	}

	private void ShootBuildUpAnimation()
	{
		if (shootBuildUp == null) return;
		SetAnimator(shootBuildUp);
	}
	
	private void ShootAnimation()
	{
		if (shoot == null) return;
		SetAnimator(shoot);
	}

	private void MeleeAnimation()
	{
		if (melee == null) return;
		SetAnimator(melee);
	}
	
	private void WalkAnimation()
	{
		if (walk == null) return;
		SetAnimator(walk, 0.2f);
	}
	
	private void RunAnimation()
	{
		if (run == null) return;
		SetAnimator(run, 0.2f);
	}
	
	private void PrepareToRunAnimation()
	{
		if (prepareToRun == null) return;
		SetAnimator(prepareToRun);
	}

	private void MovementSFX()
	{
		movementSFX.GetComponent<FMODUnity.StudioEventEmitter>().Play();
	}

	private void ScreechSFX()
	{
		screechSFX.GetComponent<FMODUnity.StudioEventEmitter>().Play();
	}

	private void meleeSFX()
	{
		FMODUnity.RuntimeManager.PlayOneShot(meleeSwishSFX);
	}
	
	private void shootingSFX()
	{
		shootSFX.GetComponent<FMODUnity.StudioEventEmitter>().Play();
	}
}
