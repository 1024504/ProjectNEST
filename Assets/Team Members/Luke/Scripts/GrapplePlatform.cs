using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePlatform : MonoBehaviour
{
    public bool movablePlatform;
    public bool constantlyMoving;
    public float moveSpeed = 1f;

    public Vector3 firstPositionWorld;
    public Vector3 secondPositionLocal;

    private Transform _transform;
    private float _progress = 0;
    public bool isMoving = false;
    public bool isGrappled = false;

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
        firstPositionWorld = _transform.position;
        if (constantlyMoving) isMoving = true;
    }

    private void FixedUpdate()
    {
        if (!isMoving) return;
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        if (_progress > 2 * Mathf.PI)
        {
            _progress = 0;
            if (!(constantlyMoving || isGrappled))
            {
                isMoving = false;
                return;
            }
        }
        
        float pathProgress = (-Mathf.Cos(_progress*moveSpeed)+1f)/2f;
        
        _transform.position = firstPositionWorld + pathProgress * secondPositionLocal;
        
        _progress += Time.fixedDeltaTime;
    }
}
