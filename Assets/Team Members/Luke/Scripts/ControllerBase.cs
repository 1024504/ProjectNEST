using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase : MonoBehaviour
{
	[SerializeField] private MonoBehaviour agent;
	
	public MonoBehaviour Agent
	{
		get => agent;
		set
		{
			if ((IControllable)value == null) return;
			DisableInputs((IControllable)agent);
			agent = value;
			EnableInputs((IControllable)agent);
		}
	}

	protected virtual void EnableInputs(IControllable iControllable)
	{
		
	}
	
	protected virtual void DisableInputs(IControllable iControllable)
	{
		
	}
}
