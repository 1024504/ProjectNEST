using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class MothPatrolState : AntAIState
{
	private AlveriumMoth _agent;
	private Coroutine _coroutine;
	private float _moveInput = 1f;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumMoth>();
		if (_agent.beginsPatrolLeft)
		{
			_moveInput = -1f;
			_agent.localDefaultAimPosition.x *= -1f;
		}
	}
	
	public override void Enter()
	{
		base.Enter();
		
		BeginWalk();
	}

	private void BeginWalk()
	{
		_coroutine = StartCoroutine(_agent.CanPatrolTimer(_agent.patrolDuration));
		_agent.aimTransform.localPosition = _agent.localDefaultAimPosition;
		_agent.MovePerformed(_moveInput);
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
		StopCoroutine(_coroutine);
		_moveInput *= -1;
		_agent.localDefaultAimPosition.x *= -1f;
	}
}
