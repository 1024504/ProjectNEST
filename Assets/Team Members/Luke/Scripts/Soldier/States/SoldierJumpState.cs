using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierJumpState : AntAIState
{
	private AlveriumSoldier _agent;
	private Vector2 _jumpTargetNormal;
	private Vector3 _jumpTargetPosition;
	private RaycastHit2D _jumpTarget;
	private float _jumpDuration;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumSoldier>();
	}

	public override void Enter()
	{
		base.Enter();
		_jumpTargetNormal = Vector2.up;
		_jumpTargetPosition = _agent.transform.position;
		_jumpDuration = 0;
		_jumpTarget = _agent.GetJumpTarget();
		if (_jumpTarget.collider != null)
		{
			Player player = _jumpTarget.collider.GetComponent<Player>();
			if (player == null)
			{
				_jumpTargetNormal = _jumpTarget.normal;
			}
			
			_jumpTargetPosition = _jumpTarget.point;
		}
		_jumpDuration = _agent.JumpToTarget(_jumpTargetPosition);
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		_agent.GetJumpTarget();
		float angle = Vector2.SignedAngle(_agent.transform.up, _jumpTargetNormal)*0.5f;
		_agent.transform.rotation = Quaternion.Euler(0, 0, _agent.transform.eulerAngles.z+angle);
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
