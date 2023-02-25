using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public float hitDistance;
    public float maxDistance;
    private void Update()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector2.right), out hitInfo,
                maxDistance))
        {
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector2.right) * hitInfo.distance, Color.blue);
            hitDistance = hitInfo.distance;
        }
    }

  
}
