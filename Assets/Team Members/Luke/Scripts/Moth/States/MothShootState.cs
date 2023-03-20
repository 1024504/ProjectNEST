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
		// StartCoroutine(AttackAnimation());
	}

	// private IEnumerator AttackAnimation()
	// {
	// 	_agent.anim.CrossFade("Attack_2", 0);
	// 	yield return new WaitForSeconds(_agent.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
	// 	_agent.CooldownAttack();
	// }

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
