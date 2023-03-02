using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierIdleState : AntAIState
{
	private AlveriumSoldier _agent;
	private Coroutine _coroutine;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumSoldier>();
	}

	public override void Enter()
	{
		base.Enter();
		_agent.currentMoveSpeed = 0;
		_agent.MoveCancelled();
		if (_agent.currentMoveSpeed > _agent.patrolSpeed)
		{
			_agent.anim.CrossFade("Lost_Player", 0);
			_coroutine = StartCoroutine(LostTarget());
		}
		else
		{
			_agent.anim.CrossFade("Idle", 0);
			_coroutine = StartCoroutine(_agent.CanPatrolTimer(_agent.idleDuration));
		}
	}
	
	private IEnumerator LostTarget()
	{
		yield return new WaitForSeconds(_agent.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		_agent.anim.CrossFade("Idle", 0);
		_coroutine = StartCoroutine(_agent.CanPatrolTimer(_agent.idleDuration));
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
	}

	public override void Exit()
	{
		base.Exit();
		StopCoroutine(_coroutine);
	}

	public override void Destroy(GameObject aGameObject)
	{
		base.Destroy(aGameObject);
	}
}
