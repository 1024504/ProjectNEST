using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : ControllerBase
{
	private enum SoldierState
	{
		Idle,
		Attacking,
		Grappled,
		Dead
	}

	[SerializeField] private SoldierState _state = SoldierState.Idle;
	
	[SerializeField] private float lateralMoveInput;


	private void OnEnable()
	{
		if ((IControllable)Agent != null) EnableInputs((IControllable)Agent);
	}
	
	private void OnDisable()
	{
		if ((IControllable)Agent != null) DisableInputs((IControllable)Agent);
	}

	private void FixedUpdate()
	{
		switch (_state)
		{
			case SoldierState.Idle:
			{
				((IControllable)Agent).MovePerformed(lateralMoveInput);
				break;
			}
			case SoldierState.Attacking:
			{
				((IControllable)Agent).MovePerformed(lateralMoveInput);
				break;
			}
			case SoldierState.Grappled:
			{
				break;
			}
			case SoldierState.Dead:
			{
				break;
			}
		}
	}
}
