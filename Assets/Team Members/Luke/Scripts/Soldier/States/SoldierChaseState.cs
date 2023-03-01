using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierChaseState : AntAIState
{
	private AlveriumSoldier _agent;
	private Transform _agentTransform;
	
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
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		_agent.MovePerformed(Mathf.Sign(_agentTransform.TransformDirection(_agent.currentTarget.position - _agentTransform.position).x));
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
