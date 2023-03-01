using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierJumpState : AntAIState
{
	private AlveriumSoldier _agent;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumSoldier>();
	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Destroy(GameObject aGameObject)
	{
		base.Destroy(aGameObject);
	}
}
