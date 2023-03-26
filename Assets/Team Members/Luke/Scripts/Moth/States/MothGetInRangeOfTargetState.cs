using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class MothGetInRangeOfTargetState : AntAIState
{
	private AlveriumMoth _agent;
	private float _verticalMovementInput;
	private float _lateralMovementInput;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumMoth>();
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);

		_verticalMovementInput = _agent.idealDistanceFromTarget/1.41f + _agent.CheckVerticalDistanceToTarget();
		
		float lateralDistance = _agent.CheckLateralDistanceToTarget();
		_lateralMovementInput = _agent.idealDistanceFromTarget/1.41f*Mathf.Sign(-lateralDistance) + lateralDistance;
		_lateralMovementInput /= Mathf.Abs(_verticalMovementInput);

		if (_verticalMovementInput > 0)
		{
			_agent.DashCancelled();
			_agent.JumpPerformed();
		}
		else
		{
			_agent.JumpCancelled();
			_agent.DashHeld();
		}
		_agent.MovePerformed(_lateralMovementInput);
	}

	public override void Exit()
	{
		base.Exit();
		
		_agent.JumpCancelled();
		_agent.DashCancelled();
	}
}
