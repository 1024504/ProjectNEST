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
		string clipName = clip.name; 
		Anim.CrossFade(clipName, 0, 0);
	}
	
	protected void SetAnimator(AnimationClip clip, float crossFadeTime)
	{
		// May need to check if the clip is already playing
		crossFadeTime = Mathf.Clamp (crossFadeTime, 0, 1);
		string clipName = clip.name; 
		Anim.CrossFade(clipName, crossFadeTime, 0);
	}
}
