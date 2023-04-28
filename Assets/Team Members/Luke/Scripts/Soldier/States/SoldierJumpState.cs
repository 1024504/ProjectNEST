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

	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumSoldier>();
	}

	public override void Enter()
	{
		base.Enter();
		_jumpTargetNormal = Vector2.up;
		Transform agentTransform = _agent.transform;
		_jumpTargetPosition = Vector3.zero;
		_jumpTarget = Physics2D.Linecast(agentTransform.position, _agent.currentTarget.position, _agent.visionLayerMask);
		if (_jumpTarget.collider != null)
		{
			Player player = _jumpTarget.collider.GetComponent<Player>();
			if (player == null)
			{
				_jumpTargetNormal = _jumpTarget.normal;
			}
			
			_jumpTargetPosition = _jumpTarget.point;
		}
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		if (!_agent.terrainDetection.isActiveAndEnabled)
		{
			Transform agentTransform = _agent.transform;
			_jumpTarget = _jumpTarget = Physics2D.Raycast(agentTransform.position, _agent.GetComponent<Rigidbody2D>().velocity, 5f, _agent.visionLayerMask);
			if (_jumpTarget.collider != null)
			{
				Player player = _jumpTarget.collider.GetComponent<Player>();
				if (player == null)
				{
					_jumpTargetNormal = _jumpTarget.normal;
				}
			}

			_agent.transform.Rotate(0,0,Mathf.Clamp(Vector2.SignedAngle(agentTransform.up, _jumpTargetNormal), -10f, 10f));
		}
		else
		{
			if (!_agent.terrainDetection.isGrounded) return;
			if (_jumpTargetPosition == Vector3.zero) return;
			_agent.JumpToTarget(_jumpTargetPosition);
		}
	}

	public override void Exit()
	{
		base.Exit();
		if (_agent.currentTarget == null) return;
		if (_agent.transform.InverseTransformDirection(_agent.currentTarget.position-_agent.transform.position).x <= 0)
		{
			if (_agent.view.localRotation.y != 0) _agent.view.localRotation = Quaternion.Euler(0, 180, 0);
			else _agent.view.localRotation = Quaternion.Euler(Vector3.zero);
		}
	}

	public override void Destroy(GameObject aGameObject)
	{
		base.Destroy(aGameObject);
	}
}
