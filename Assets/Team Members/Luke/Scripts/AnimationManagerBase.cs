using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagerBase : MonoBehaviour
{
	protected Animator Anim;
	
	protected virtual void OnEnable()
	{
		Anim = GetComponent<Animator>();
	}
	
	protected void SetAnimator(AnimationClip clip)
	{
		Anim.CrossFade(clip.name, 0, 0);
	}
	
	protected void SetAnimator(AnimationClip clip, float crossFadeTime)
	{
		// May need to check if the clip is already playing
		crossFadeTime = Mathf.Clamp (crossFadeTime, 0, 1);
		Anim.CrossFade(clip.name, crossFadeTime, 0);
	}
}
