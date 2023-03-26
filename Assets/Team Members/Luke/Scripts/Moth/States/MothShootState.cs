using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class MothShootState : AntAIState
{
	private AlveriumMoth _agent;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumMoth>();
	}

	public override void Enter()
	{
		base.Enter();
		_agent.MoveCancelled();
		_agent.JumpCancelled();
		_agent.DashCancelled();
		StartCoroutine(AttackAnimation());
	}

	private IEnumerator AttackAnimation()
	{
		_agent.OnAttackBuildup?.Invoke();
		yield return new WaitForSeconds(_agent.anim.GetCurrentAnimatorStateInfo(0).length);
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

	public override void Destroy(GameObject aGameObject)
	{
		base.Destroy(aGameObject);
	}
}
