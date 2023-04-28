using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierTerrainDetection : TerrainDetection
{
    public float lateralMoveInput;
    
    public List<Collider2D> _groundColliders = new ();

    private void FixedUpdate()
    {
	    Vector3 position = _t.position + _t.TransformDirection(new Vector3(lateralMoveInput*1f, 0, 0));
        
        if (_groundColliders.Count > 0)
        {
	        int c = 0;
	        for (int i = 0; i < _groundColliders.Count; i++)
	        {
		        if (_groundColliders[i+c] != null) continue;
		        _groundColliders.Remove(_groundColliders[i+c]);
		        c--;
		        if (_groundColliders.Count == 0)
		        {
			        isGrounded = false;
			        return;
		        }
	        }
	        
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
            mainNormal = Physics2D.Linecast(position, _groundColliders[closestIndex].ClosestPoint(position)).normal;
        }
        else
        {
            isGrounded = false;
            mainNormal = Vector2.up;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
	    if (!_groundColliders.Contains(other)) _groundColliders.Add(other);
	    isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
	    if (_groundColliders.Contains(other)) _groundColliders.Remove(other);
    }
}
