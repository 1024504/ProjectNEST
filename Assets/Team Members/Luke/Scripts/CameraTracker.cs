using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
	private Transform _cameraTransform;
	public Transform playerTransform;

	[Range(0,5)]
	public float cameraSpeed = 1f;
	public float cameraDistance = 10f;

	private void OnEnable()
    {
	    _cameraTransform = transform;
    }

    private void FixedUpdate()
    {
	    _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, playerTransform.position+cameraDistance*Vector3.back, 0.1f*cameraSpeed);
    }
}
