using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public float maxDistance;
    private Transform _transform;
    private Transform _laserTransform;
    private void OnEnable()
    {
	    _transform = transform;
	    _laserTransform = GetComponentInChildren<SpriteRenderer>().transform;
    }
    
    public void DefaultSize()
	{
	   ChangeSize(maxDistance);
	}

    public void NewSize(Vector3 targetPosition)
    {
	    Vector3 heading = targetPosition - _transform.position;
	    ChangeSize(Mathf.Min(maxDistance, heading.magnitude));
    }

    private void ChangeSize(float newSize)
    {
	    Vector3 laserLocalScale = _laserTransform.localScale;
	    _laserTransform.localScale = new Vector3(newSize, laserLocalScale.y, laserLocalScale.z);
	    _laserTransform.localPosition = new Vector3(newSize/2f, 0, 0);
    }
}
