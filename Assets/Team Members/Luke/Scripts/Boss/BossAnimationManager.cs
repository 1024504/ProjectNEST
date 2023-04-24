using System.Collections;
using System.Collections.Generic;
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

	public GameObject shootDistanceSFX;

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

	private void ShootSFX()
	{
		shootDistanceSFX.GetComponent<FMODUnity.StudioEventEmitter>().Play();
	}
}
