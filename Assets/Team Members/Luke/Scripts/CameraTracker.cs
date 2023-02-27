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

	[Range(0,5)]
	public float cameraSpeed = 1f;
	public float cameraDistance = 10f;

	private void OnEnable()
	{
		_cam = GetComponent<Camera>();
	    _cameraTransform = transform;
	    _collider = GetComponent<BoxCollider2D>();
	    UpdateCollider();
    }

    private void FixedUpdate()
    {
	    _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, playerTransform.position+cameraDistance*Vector3.back, 0.1f*cameraSpeed);
    }

    public void UpdateCollider()
    {
	    _collider.size = new Vector2(2*_cam.orthographicSize*_cam.aspect, 2*_cam.orthographicSize);
    }
}
