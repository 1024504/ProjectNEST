using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class BossPrepareToRunState : AntAIState
{
	private AlveriumBoss _agent;
	private Coroutine _coroutine;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumBoss>();
	}

	public override void Enter()
	{
		base.Enter();
		_agent.MoveCancelled();
		_agent.currentMoveSpeed = _agent.runSpeed;
		_coroutine = StartCoroutine(_agent.PrepareToRun());
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
}
