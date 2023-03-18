using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
	public AnimationClip idle;
	public AnimationClip walk;
	public AnimationClip jump;

	private Animator _anim;
	private Player _player;

	private void OnEnable()
	{
		_anim = GetComponent<Animator>();
		_player = GetComponentInParent<Player>();
		_player.OnPlayerWalk += WalkAnimation;
		_player.OnPlayerIdle += IdleAnimation;
		_player.OnPlayerJump += JumpAnimation;
	}

	private void SetAnimator(AnimationClip clip)
	{
		_anim.CrossFade(clip.name, 0, 0);
	}
	
	private void WalkAnimation()
	{
		SetAnimator(walk);
	}
	
	private void IdleAnimation()
	{
		SetAnimator(idle);
	}
	
	private void JumpAnimation()
	{
		SetAnimator(jump);
	}
}
