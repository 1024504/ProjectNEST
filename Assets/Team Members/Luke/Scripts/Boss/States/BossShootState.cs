using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class BossShootState : AntAIState
{
	private AlveriumBoss _agent;
	private bool _hasShot;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumBoss>();
	}

	public override void Enter()
	{
		base.Enter();
		_agent.MoveCancelled();
		_agent.canSwapMode = false;
		_hasShot = false;
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		if (_hasShot) return;
		
		if (_agent.tailAngle <= _agent.minTailAngle) return;
		_agent.ShootPerformed();
		_hasShot = true;
	}

	public override void Exit()
	{
		base.Exit();
		
	}
}
