using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamageHealth : MonoBehaviour
{
    public float damageAmount;

    private void FixedUpdate()
    {
        Vector3 position3D = transform.position;
        
        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(position3D.x, position3D.y), Vector2.right);

        if (!hitInfo) return;
        Debug.Log(hitInfo.collider.gameObject.name);
        
        // Health otherHealth = hitInfo.collider.GetComponent<Health>();
        
        // if (otherHealth == null) return;
        // otherHealth.HealthLevel -= damageAmount;
    }
}
