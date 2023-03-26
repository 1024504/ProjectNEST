using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MothVisionTrigger : MonoBehaviour
{
	private AlveriumMoth _agent;
	private Coroutine _coroutine;
	private float _radius;


	private void OnEnable()
	{
		_agent = GetComponentInParent<AlveriumMoth>();
		_radius = GetComponent<CircleCollider2D>().radius;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player == null) return;
		Transform target = player.transform;
		if (!_agent.targetLocations.Contains(target))
		{
			_agent.targetLocations.Add(target);
			StartTimer(target);
		}
		else
		{
			StopCoroutine(_coroutine);
			StartTimer(target);
		}
	}

	private void StartTimer(Transform target)
	{
		_coroutine = StartCoroutine(TargetMemory(target));
	}

	private IEnumerator TargetMemory(Transform target)
	{
		yield return new WaitForSeconds(_agent.memoryDuration);
		if (target == null)
		{
			Debug.Log("Null Target");
		}
		else if (Vector3.Distance(target.position, _agent.transform.position) > _radius) _agent.targetLocations.Remove(target);
		else StartTimer(target);
	}
}
