using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    public virtual void OnStateEnter()
	{
		
	}
    
    public virtual void OnStateUpdate()
	{
		
	}

    public virtual void OnStateFixedUpdate()
    {
	    
    }

	public virtual void OnStateExit()
	{
		
	}
}