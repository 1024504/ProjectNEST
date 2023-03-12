using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
	private Camera _cam;
	private Transform _cameraTransform;
	public Transform playerTransform;
	private BoxCollider2D _collider;

	[Header("Follow Variables")]
	public float f = 1f;
	public float zeta = 1f;
	public float r = 0f;
	
	private Vector3 _nextPosition = Vector3.zero;
	private Vector3 _currentVelocity = Vector3.zero;
	private Vector3 _playerVelocity = Vector3.zero;
	private Vector3 _playerPrevPosition = Vector3.zero;

	[Range(0,5)]
	public float cameraSpeed = 1f;
	[SerializeField] private float cameraSize = 10f;
	
	public float CameraSize
	{
		get => cameraSize;
		set
		{
			cameraSize = value;
			_cam.orthographicSize = cameraSize;
			UpdateCollider();
		}
	}
	
	private void OnEnable()
	{
		_cam = GetComponent<Camera>();
	    _cameraTransform = transform;
	    _cam.orthographicSize = cameraSize;
	    _collider = GetComponent<BoxCollider2D>();
	    UpdateCollider();
	    _playerPrevPosition = playerTransform.position+Vector3.back;
    }
	
	private Vector3 NextStep(float timeStep, Vector3 x, Vector3 xd)
	{
		float constant1 = zeta / (Mathf.PI * f);
		float constant2Stable = Mathf.Max(1 / (2*Mathf.PI * f * 2*Mathf.PI * f), 1.1f * (timeStep*timeStep/4+timeStep*constant1/2));
		_nextPosition = _cameraTransform.position + timeStep * _currentVelocity;
		_currentVelocity += timeStep * (x + r *zeta / (2*Mathf.PI * f) * xd - _nextPosition - constant1 * _currentVelocity) / constant2Stable;
		return _nextPosition;
	}

	public void FixedUpdate()
	{
		Vector3 position = playerTransform.position;
		_cameraTransform.position = NextStep(Time.fixedDeltaTime, position+Vector3.back, _playerVelocity);
		_playerVelocity = (position+Vector3.back - _playerPrevPosition) / Time.fixedDeltaTime;
		_playerPrevPosition = position+Vector3.back;
	}

    /*private void FixedUpdate()
    {
	    _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, playerTransform.position+Vector3.back, 0.1f*cameraSpeed);
    }*/

    public void UpdateCollider()
    {
	    _collider.size = new Vector2(2*_cam.orthographicSize*_cam.aspect, 2*_cam.orthographicSize);
    }
}
