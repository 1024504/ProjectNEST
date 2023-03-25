using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAnimationManager : MonoBehaviour
{
	public AnimationClip idle;
	public AnimationClip moveBurst;
	public AnimationClip moveConstant;
	public AnimationClip attackBuildup;
	public AnimationClip attackShoot;

	private Animator _anim;
	private AlveriumMoth _moth;

	private void OnEnable()
	{
		_anim = GetComponent<Animator>();
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

	private void SetAnimator(AnimationClip clip)
	{
		_anim.CrossFade(clip.name, 0, 0);
	}
	
	private void IdleAnimation()
	{
		if (idle == null) return;
		SetAnimator(idle);
	}
	
	private void MoveBurstAnimation()
	{
		if (moveBurst == null) return;
		SetAnimator(moveBurst);
	}

	private void MoveConstantAnimation()
	{
		if (moveConstant == null) return;
		SetAnimator(moveConstant);
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
