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
	private bool _isConnected;
	public Action<Transform> OnHit;

	private Transform _t;

	[SerializeField]
	private Transform ropeTransform;
	private Renderer _ropeRenderer;
	public float ropeWiggliness = 12;
	public float ropeWidth = 5f;

	private void OnEnable()
	{
		_t = transform;
		_ropeRenderer = ropeTransform.GetComponent<Renderer>();
		_ropeRenderer.enabled = false;
	}

	public void FixedUpdate()
	{
		if (_hookGO == null) return;
		Vector3 position = _t.position;
		Vector3 hookDirection = _hookGO.transform.position - position;
		ropeTransform.position = position+hookDirection*0.5f;
		ropeTransform.localScale = new Vector3(ropeWidth, hookDirection.magnitude, 1);
		ropeTransform.rotation = Quaternion.LookRotation(Vector3.forward, hookDirection);
		if (_isConnected) return;
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
		_ropeRenderer.enabled = true;
		_grappleTimer = StartCoroutine(ResetTimer(cooldown));
	}

	private void GrappleHit(Transform grappleHitTransform)
	{
		_isConnected = true;
		_ropeRenderer.material.SetFloat("_Speed",0);
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
		_isConnected = false;
		_ropeRenderer.material.SetFloat("_Speed",ropeWiggliness);
		_ropeRenderer.enabled = false;
		ropeTransform.localScale = new Vector3(ropeWidth, 0, 1);
		_canGrapple = true;
	}
}
