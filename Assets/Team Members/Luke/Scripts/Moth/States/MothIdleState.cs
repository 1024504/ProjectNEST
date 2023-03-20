using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class MothIdleState : AntAIState
{
	private AlveriumMoth _agent;
	private Coroutine _coroutine;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumMoth>();
	}

	public override void Enter()
	{
		base.Enter();
		_agent.MoveCancelled();
		_agent.OnIdle?.Invoke();
		_coroutine = StartCoroutine(_agent.CanPatrolTimer(_agent.idleDuration));
	}
	
	public override void Exit()
	{
		base.Exit();
		StopCoroutine(_coroutine);
	}
}
