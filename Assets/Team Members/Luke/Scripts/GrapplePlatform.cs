using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePlatform : MonoBehaviour
{
    public bool movablePlatform;
    public bool constantlyMoving;
    public float moveSpeed;

    private Vector3 _firstPosition;
    public Vector3 secondPosition;

    private Transform _transform;
    private float _progress = 0;
    private bool _isMoving = false;
    private bool _isGrappled = false;

    public bool IsGrappled
    {
        get => _isGrappled;
        set
        {
            _isGrappled = true;
            _isMoving = true;
        }
    }

    private float Progress
    {
        get => _progress;
        set
        {
            _progress = value;
            if (_progress > 2 * Mathf.PI) _progress -= 2 * Mathf.PI;
        }
    }

    private void OnEnable()
    {
        _transform = transform;
        _firstPosition = _transform.position;
        if (constantlyMoving) _isMoving = true;
    }

    private void FixedUpdate()
    {
        if (!_isMoving) return;
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        if (_progress > 2 * Mathf.PI)
        {
            _progress = 0;
            if (constantlyMoving !| _isGrappled)
            {
                _isMoving = false;
                return;
            }
        }
        
        Vector3 secondPositionVector = secondPosition-_firstPosition;

        _transform.position = _firstPosition + Mathf.Sin(_progress) * secondPositionVector;
        
        _progress += Time.fixedDeltaTime;
    }
}
