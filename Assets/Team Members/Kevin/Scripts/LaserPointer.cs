using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public float hitDistance;
    public float maxDistance;
    private RaycastHit2D hit2D;
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(maxDistance,1f);
    }
    private void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector2.right), out hitInfo,
                maxDistance))
        {
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector2.right) * hitInfo.distance, Color.blue);
            hitDistance = hitInfo.distance;
            gameObject.transform.localScale = new Vector3(hitDistance,1f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(maxDistance,1f);
        }
        

        /*hit2D = Physics2D.Raycast(transform.position, Vector2.right, maxDistance);

        if (hit2D.collider != null)
        {
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector2.right),Color.green);
            hitDistance = hit2D.distance;
            //hitDistance = hit2D.distance;
            Debug.Log(hit2D.distance);
            gameObject.transform.localScale = new Vector3(hitDistance, 1f);
        }
        else
        {
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector2.right)*maxDistance,Color.green);
            gameObject.transform.localScale = new Vector3(maxDistance,1f);
        }*/
    }

  
}
