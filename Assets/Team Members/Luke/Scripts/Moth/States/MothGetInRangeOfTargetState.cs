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


		if (Mathf.Abs(_verticalMovementInput) < 1f)
		{
			_agent.DashCancelled();
			_agent.JumpCancelled();
		}
		else if (_verticalMovementInput > 0)
		{
			_agent.DashCancelled();
			_agent.JumpPerformed();
		}
		else
		{
			_agent.JumpCancelled();
			_agent.DashHeld();
		}

		if (Mathf.Abs(_lateralMovementInput) < 1f)
		{
			_agent.MoveCancelled();
			_agent.OnIdle?.Invoke();
			return;
		}
		_agent.MovePerformed(_lateralMovementInput);
		StartCoroutine(BurstAccelerate());
	}

	private IEnumerator BurstAccelerate()
	{
		_agent.OnMoveBurst?.Invoke();
		string currentStateName = _agent.anim.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
		yield return new WaitWhile(() => _agent.anim.GetCurrentAnimatorStateInfo(0).IsName(currentStateName));
		yield return new WaitForSeconds(_agent.anim.GetCurrentAnimatorStateInfo(0).length);
		_agent.OnMoveConstant?.Invoke();
	}

	public override void Exit()
	{
		base.Exit();
		
		_agent.JumpCancelled();
		_agent.DashCancelled();
	}
}
