using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimationManager : MonoBehaviour
{
	public AnimationClip idle;
	public AnimationClip walk;
	public AnimationClip attack;
	public AnimationClip lostTarget;
	public AnimationClip spottedTarget;
	public AnimationClip run;

	private Animator _anim;
	private AlveriumSoldier _soldier;

	private void OnEnable()
	{
		_anim = GetComponent<Animator>();
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

	private void SetAnimator(AnimationClip clip)
	{
		_anim.CrossFade(clip.name, 0, 0);
	}
	
	private void IdleAnimation()
	{
		SetAnimator(idle);
	}
	
	private void WalkAnimation()
	{
		SetAnimator(walk);
	}

	private void AttackAnimation()
	{
		SetAnimator(attack);
	}
	
	private void LostTargetAnimation()
	{
		SetAnimator(lostTarget);
	}
	
	private void SpottedTargetAnimation()
	{
		SetAnimator(spottedTarget);
	}
	
	private void RunAnimation()
	{
		SetAnimator(run);
	}
}
