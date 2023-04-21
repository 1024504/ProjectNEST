using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoldierAnimationManager : AnimationManagerBase
{
	public AnimationClip idle;
	public AnimationClip walk;
	public AnimationClip attack;
	public AnimationClip lostTarget;
	public AnimationClip spottedTarget;
	public AnimationClip run;

	private AlveriumSoldier _soldier;

	public GameObject MovementdistanceSFX;
	public GameObject SnarlDistanceSFX;
	public GameObject ScreechDistanceSFX;

	protected override void OnEnable()
	{
		base.OnEnable();
		_soldier = GetComponentInParent<AlveriumSoldier>();
		_soldier.OnIdle += IdleAnimation;
		_soldier.OnWalk += WalkAnimation;
		_soldier.OnAttack += AttackAnimation;
		_soldier.OnLostTarget += LostTargetAnimation;
		_soldier.OnSpotTarget += SpottedTargetAnimation;
		_soldier.OnRun += RunAnimation;
	}
	
	private void OnDisable()
	{
		_soldier.OnIdle -= IdleAnimation;
		_soldier.OnWalk -= WalkAnimation;
		_soldier.OnAttack -= AttackAnimation;
		_soldier.OnLostTarget -= LostTargetAnimation;
		_soldier.OnSpotTarget -= SpottedTargetAnimation;
		_soldier.OnRun -= RunAnimation;
	}

	private void IdleAnimation()
	{
		if (idle == null) return;
		SetAnimator(idle);
	}
	
	private void WalkAnimation()
	{
		if (walk == null) return;
		SetAnimator(walk);
	}

	private void AttackAnimation()
	{
		if (attack == null) return;
		SetAnimator(attack);
	}
	
	private void LostTargetAnimation()
	{
		if (lostTarget == null) return;
		SetAnimator(lostTarget);
	}
	
	private void SpottedTargetAnimation()
	{
		if (spottedTarget == null) return;
		SetAnimator(spottedTarget);
	}
	
	private void RunAnimation()
	{
		if (run == null) return;
		SetAnimator(run);
	}

	private void MovementSFX()
    {
		MovementdistanceSFX.GetComponent<FMODUnity.StudioEventEmitter>().Play();
	}

	private void SnarlSFX()
	{
		SnarlDistanceSFX.GetComponent<FMODUnity.StudioEventEmitter>().Play();
	}

	private void ScreechSFX()
	{
		ScreechDistanceSFX.GetComponent<FMODUnity.StudioEventEmitter>().Play();
	}
}
