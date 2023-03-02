using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierChaseState : AntAIState
{
	private AlveriumSoldier _agent;
	private Transform _agentTransform;
	private Coroutine _coroutine;
	private bool _animationFinished = false;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumSoldier>();
		_agentTransform = aGameObject.transform;
	}

	public override void Enter()
	{
		base.Enter();
		_agent.currentMoveSpeed = _agent.chaseSpeed;
		_agent.MoveCancelled();
		_agent.anim.CrossFade("Spotting_Player", 0);
		_coroutine = StartCoroutine(SpottingTarget());
	}

	private IEnumerator SpottingTarget()
	{
		yield return new WaitForSeconds(_agent.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		_agent.anim.CrossFade("Run", 0);
		_animationFinished = true;
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		if(_animationFinished) _agent.MovePerformed(Mathf.Sign(_agentTransform.TransformDirection(_agent.currentTarget.position - _agentTransform.position).x));
	}

	public override void Exit()
	{
		base.Exit();
		_animationFinished = false;
		StopCoroutine(_coroutine);
	}

	public override void Destroy(GameObject aGameObject)
	{
		base.Destroy(aGameObject);
	}
}
