using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTerrainCollider : TerrainCollider
{
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
        Vector3 position = _t.position;

        if (_groundColliders.Count > 0)
        {
            float closestDistance = Vector2.Distance(position, _groundColliders[0].ClosestPoint(position));
            int closestIndex = 0;

            if (_groundColliders.Count > 1)
            {
                for (int i = 1; i < _groundColliders.Count-1; i++)
                {
                    if (Vector2.Distance(position, _groundColliders[i].ClosestPoint(position)) < closestDistance)
                        closestIndex = i;
                }
            }
            normal = Physics2D.Linecast(position, _groundColliders[closestIndex].ClosestPoint(position)).normal;
            Debug.DrawRay(_groundColliders[closestIndex].ClosestPoint(position),normal, Color.green);
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