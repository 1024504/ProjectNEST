using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierPatrolState : AntAIState
{
	private AlveriumSoldier _agent;
	private Coroutine _coroutine;
	private float _moveInput = 1f;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumSoldier>();
		if(_agent.beginsPatrolLeft) _moveInput = -1f;
	}

	public override void Enter()
	{
		base.Enter();
		if (_agent.currentMoveSpeed > _agent.patrolSpeed)
		{
			_agent.MoveCancelled();
			_agent.anim.CrossFade("Lost_Player", 0);
			_coroutine = StartCoroutine(LostTarget());
		}
		else BeginWalk();
		_agent.currentMoveSpeed = _agent.patrolSpeed;
	}
	
	private IEnumerator LostTarget()
	{
		yield return new WaitForSeconds(_agent.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		BeginWalk();
	}
	
	private void BeginWalk()
	{
		_coroutine = StartCoroutine(_agent.CanPatrolTimer(_agent.patrolDuration));
		_agent.MovePerformed(_moveInput);
		_agent.anim.CrossFade("Walk", 0);
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
	}

	public override void Exit()
	{
		base.Exit();
		StopCoroutine(_coroutine);
		_moveInput *= -1;
	}

	public override void Destroy(GameObject aGameObject)
	{
		base.Destroy(aGameObject);
	}
}
