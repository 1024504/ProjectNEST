using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierTerrainCollider : TerrainCollider
{
    public float lateralMoveInput;
    
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    
    private void FixedUpdate()
    {
        Vector3 position = _t.position + _t.TransformDirection(new Vector3(lateralMoveInput*1f, 0, 0));
        
        if (_groundColliders.Count > 0)
        {
            float closestDistance = Vector2.Distance(position, _groundColliders[0].ClosestPoint(position));
            int closestIndex = _groundColliders.Count-1;
        
            if (_groundColliders.Count > 1)
            {
                for (int i = _groundColliders.Count-2; i >= 0; i--)
                {
                    if (Vector2.Distance(position, _groundColliders[i].ClosestPoint(position)) < closestDistance)
                        closestIndex = i;
                }
            }
            normal = Physics2D.Linecast(position, _groundColliders[closestIndex].ClosestPoint(position)).normal;
        }
        else
        {
            isGrounded = false;
            normal = Vector2.up;
        }
    }
    
    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
}
