using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class MothGetInRangeOfTargetState : AntAIState
{
	private AlveriumMoth _agent;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		_agent = aGameObject.GetComponent<AlveriumMoth>();
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
