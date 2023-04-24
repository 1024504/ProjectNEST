using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class BossMeleeAttackState : AntAIState
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
		_agent.canSwapMode = false;
		_agent.MoveCancelled();
		_agent.Action1Performed();
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		
	}

	public override void Exit()
	{
		base.Exit();
		_agent.canSwapMode = true;
		if (_agent.health.HealthLevel > _agent.health.maxHealth * 0.5f) _agent.canRun = false;
	}
}
