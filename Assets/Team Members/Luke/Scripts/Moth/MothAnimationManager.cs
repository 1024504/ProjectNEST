using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MothAnimationManager : AnimationManagerBase
{
	public AnimationClip idle;
	public AnimationClip moveBurst;
	public AnimationClip moveConstant;
	public AnimationClip attackBuildup;
	public AnimationClip attackShoot;

	private AlveriumMoth _moth;

	protected override void OnEnable()
	{
		base.OnEnable();
		_moth = GetComponentInParent<AlveriumMoth>();
		_moth.OnIdle += IdleAnimation;
		_moth.OnMoveBurst += MoveBurstAnimation;
		_moth.OnMoveConstant += MoveConstantAnimation;
		_moth.OnAttackBuildup += AttackBuildupAnimation;
		_moth.OnAttackShoot += AttackShootAnimation;
	}
	
	private void OnDisable()
	{
		_moth.OnIdle -= IdleAnimation;
		_moth.OnMoveBurst -= MoveBurstAnimation;
		_moth.OnMoveConstant -= MoveConstantAnimation;
		_moth.OnAttackBuildup -= AttackBuildupAnimation;
		_moth.OnAttackShoot -= AttackShootAnimation;
	}

	private void IdleAnimation()
	{
		if (idle == null) return;
		SetAnimator(idle, 0.2f);
	}
	
	private void MoveBurstAnimation()
	{
		if (moveBurst == null) return;
		SetAnimator(moveBurst, 0.2f);
	}

	private void MoveConstantAnimation()
	{
		if (moveConstant == null) return;
		SetAnimator(moveConstant, 0.2f);
	}
	
	private void AttackBuildupAnimation()
	{
		if (attackBuildup == null) return;
		SetAnimator(attackBuildup);
	}
	
	private void AttackShootAnimation()
	{
		if (attackShoot == null) return;
		SetAnimator(attackShoot);
	}
}
