using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
	public GameObject hookPrefab;
	private float _grappleRange;
	
	private GameObject _hookGO;
	private Coroutine _grappleTimer;
	private bool _canGrapple = true;
	public Action<Transform> OnHit;

	private Transform _t;

	private void OnEnable()
	{
		_t = transform;
	}

	public void FixedUpdate()
	{
		if (_hookGO == null) return;
		if (Vector2.Distance(_t.position, _hookGO.transform.position) > _grappleRange) ResetGrapple();
	}

	public void Shoot(float grappleVelocity, float range, float cooldown)
	{
		if (!_canGrapple) return;
		_canGrapple = false;
		_grappleRange = range;
		_hookGO = Instantiate(hookPrefab, _t.position, _t.rotation);
		Hook hook = _hookGO.GetComponent<Hook>();
		hook.OnHit += GrappleHit;
		hook.SetVelocity(grappleVelocity);
		
		_grappleTimer = StartCoroutine(ResetTimer(cooldown));
	}

	private void GrappleHit(Transform grappleHitTransform)
	{
		StopCoroutine(_grappleTimer);
		OnHit?.Invoke(grappleHitTransform);
	}
	private IEnumerator ResetTimer(float cooldown)
	{
		yield return new WaitForSeconds(cooldown);
		ResetGrapple();
	}

	public void ResetGrapple()
	{
		if (_grappleTimer != null) StopCoroutine(_grappleTimer);
		
		if (_hookGO != null)
		{
			Hook hook = _hookGO.GetComponent<Hook>();
			hook.OnHit -= GrappleHit;
			Destroy(_hookGO);
		}
		_canGrapple = true;
	}
}
