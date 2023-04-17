using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierAttackState : AntAIState
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
		_agent.MoveCancelled();
		StartCoroutine(AttackAnimation());
	}

	private IEnumerator AttackAnimation()
	{
		_agent.OnAttack?.Invoke();
		string currentStateName = _agent.anim.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
		yield return new WaitWhile(() => _agent.anim.GetCurrentAnimatorStateInfo(0).IsName(currentStateName));
		yield return new WaitForSeconds(_agent.anim.GetCurrentAnimatorStateInfo(0).length);
		_agent.CooldownAttack();
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
