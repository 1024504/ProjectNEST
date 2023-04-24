using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class BossRunToTargetState : AntAIState
{
	private AlveriumBoss _agent;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumBoss>();
	}

	public override void Enter()
	{
		base.Enter();
		_agent.currentMoveSpeed = _agent.runSpeed;
		_agent.OnRun?.Invoke();
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		if (_agent.targetInMeleeRange) _agent.MoveCancelled();
		else if (_agent.canRun) _agent.MovePerformed((_agent.target.position-_agent.transform.position).x);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
