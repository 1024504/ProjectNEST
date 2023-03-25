using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerAnimationManager : MonoBehaviour
{
	public AnimationClip idle;
	public AnimationClip walkForwards;
	public AnimationClip walkBackwards;
	public AnimationClip jump;
	public AnimationClip midAirFalling;
	public AnimationClip landing;
	public AnimationClip dash;
	public AnimationClip sprint;

	private Animator _anim;
	private Player _player;

	public EventReference footStepClip;

	private void OnEnable()
	{
		_anim = GetComponent<Animator>();
		_player = GetComponentInParent<Player>();
		_player.OnPlayerWalkForwards += WalkForwardsAnimation;
		_player.OnPlayerWalkBackwards += WalkBackwardsAnimation;
		_player.OnPlayerIdle += IdleAnimation;
		_player.OnPlayerJump += JumpAnimation;
		_player.OnPlayerMidAirFalling += MidAirFallingAnimation;
		_player.OnPlayerLanding += LandingAnimation;
		_player.OnPlayerDash += DashAnimation;
		_player.OnPlayerSprint += SprintAnimation;
	}
	
	private void OnDisable()
	{
		_player.OnPlayerWalkForwards -= WalkForwardsAnimation;
		_player.OnPlayerWalkBackwards -= WalkBackwardsAnimation;
		_player.OnPlayerIdle -= IdleAnimation;
		_player.OnPlayerJump -= JumpAnimation;
		_player.OnPlayerMidAirFalling -= MidAirFallingAnimation;
		_player.OnPlayerLanding -= LandingAnimation;
		_player.OnPlayerDash -= DashAnimation;
		_player.OnPlayerSprint -= SprintAnimation;
	}

	private void SetAnimator(AnimationClip clip)
	{
		_anim.CrossFade(clip.name, 0, 0);
	}
	
	private void WalkForwardsAnimation()
	{
		if (walkForwards == null) return;
		SetAnimator(walkForwards);
	}
	
	private void WalkBackwardsAnimation()
	{
		if (walkBackwards == null) return;
		SetAnimator(walkBackwards);
	}
	
	private void IdleAnimation()
	{
		if (idle == null) return;
		SetAnimator(idle);
	}
	
	private void JumpAnimation()
	{
		if (jump == null) return;
		SetAnimator(jump);
	}
	
	private void MidAirFallingAnimation()
	{
		if (midAirFalling == null) return;
		SetAnimator(midAirFalling);
	}
	
	private void LandingAnimation()
	{
		if (landing == null) return;
		SetAnimator(landing);
	}
	
	private void DashAnimation()
	{
		if (dash == null) return;
		SetAnimator(dash);
	}
	
	private void SprintAnimation()
	{
		if (sprint == null) return;
		SetAnimator(sprint);
	}

	// Called by timeline event
	public void FootStepSFX()
    {
		RuntimeManager.PlayOneShot(footStepClip);
    }
}
