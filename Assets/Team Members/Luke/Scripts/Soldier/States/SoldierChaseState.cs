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
		_agent.OnSpotTarget?.Invoke();
		_coroutine = StartCoroutine(SpottingTarget());
	}

	private IEnumerator SpottingTarget()
	{
		yield return new WaitForSeconds(_agent.anim.GetCurrentAnimatorStateInfo(0).length);
		_agent.OnRun?.Invoke();
		_animationFinished = true;
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		if (!_animationFinished) return;
		
		if (_agent.lastKnownTargetDirection != 0)  _agent.MovePerformed(_agent.lastKnownTargetDirection);
		else
		{
			_agent.MoveCancelled();
			// check absolute distance for canAttack
			// if canAttack, transition to attack
			// if !canAttack, start timer for can't reach target
			// at end of timer, if can still sense player, jump
		}
	}

	public override void Exit()
	{
		base.Exit();
		_agent.MoveCancelled();
		_animationFinished = false;
		StopCoroutine(_coroutine);
	}

	public override void Destroy(GameObject aGameObject)
	{
		base.Destroy(aGameObject);
	}
}
