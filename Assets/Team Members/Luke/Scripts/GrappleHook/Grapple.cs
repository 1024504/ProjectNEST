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
	public Action<Vector3> OnHit;

	private Transform _t;

	private void OnEnable()
	{
		_t = transform;
	}

	public void FixedUpdate()
	{
		if (_hookGO == null) return;
		if (Vector2.Distance(_t.position, _hookGO.transform.position) > _grappleRange) ResetGrapple();
		Debug.Log(_t.rotation.eulerAngles);
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

	private void GrappleHit(Vector3 grapplePoint)
	{
		StopCoroutine(_grappleTimer);
		OnHit?.Invoke(grapplePoint);
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
