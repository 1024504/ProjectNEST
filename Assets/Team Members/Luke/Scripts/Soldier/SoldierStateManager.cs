using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStateManager : MonoBehaviour
{
	public enum SoldierStates
	{
		Idle,
		Patrolling,
		Attacking,
		Grappled,
		Dead
	}
	
	public StateBase[] states;
	
	private StateBase _currentState;
	
	public StateBase CurrentState
	{
		get => _currentState;
		set
		{
			if (_currentState != null) _currentState.OnStateExit();
			_currentState = value;
			_currentState.OnStateEnter();
		}
	}
	
	private void Start()
	{
		CurrentState = states[0];
	}
	
	private void Update()
	{
		CurrentState.OnStateUpdate();
	}
	
	private void FixedUpdate()
	{
		CurrentState.OnStateFixedUpdate();
	}
}
