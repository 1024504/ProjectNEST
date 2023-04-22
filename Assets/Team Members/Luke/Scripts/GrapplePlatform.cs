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
    }

    private void FixedUpdate()
    {
	    if (!movablePlatform) return;
        if (isMoving || constantlyMoving) MovePlatform();
    }

    private void MovePlatform()
    {
	    if (constantlyMoving) isMoving = true;
        if (_progress > 2 * Mathf.PI || _progress < 0)
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
        
        if ( _progress > Mathf.PI || isGrappled || constantlyMoving) _progress += Time.fixedDeltaTime;
        else _progress -= Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
	    Hook hook = col.GetComponent<Hook>();
	    if (hook == null) return;
	    hook.ConnectGrapplePlatform(this);
    }
}
