using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class BossShootState : AntAIState
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
		_agent.ShootPerformed();
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		
	}

	public override void Exit()
	{
		base.Exit();
		
	}
}
