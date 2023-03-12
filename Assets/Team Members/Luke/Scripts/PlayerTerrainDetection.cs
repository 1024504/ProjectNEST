using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTerrainDetection : TerrainDetection
{
	public float raycastDownDistance = 0.1f;
	public float raycastSideDistance = 0.5f;
	public LayerMask groundLayerMask;

	private Vector2 _leftRaycastLocalOrigin;
	private Vector2 _rightRaycastLocalOrigin;

	public float leftAngle;
	public float rightAngle;
	
	protected override void OnEnable()
	{
		base.OnEnable();
		GetRaycastOrigins();
	}

	private void GetRaycastOrigins()
	{
		Bounds colliderBounds = GetComponentInParent<BoxCollider2D>().bounds;
		_leftRaycastLocalOrigin = _t.InverseTransformPoint(new Vector2(colliderBounds.min.x, colliderBounds.min.y));
		_rightRaycastLocalOrigin = _t.InverseTransformPoint(new Vector2(colliderBounds.max.x, colliderBounds.min.y));
	}

	private void FixedUpdate()
	{
		RaycastHit2D hitDataLeft = Physics2D.Raycast(_t.TransformPoint(_leftRaycastLocalOrigin), Vector2.down, raycastDownDistance, groundLayerMask);
		RaycastHit2D hitDataRight = Physics2D.Raycast(_t.TransformPoint(_rightRaycastLocalOrigin), Vector2.down, raycastDownDistance, groundLayerMask);

		if (hitDataLeft || hitDataRight)
		{
			isGrounded = true;
			if (hitDataLeft && hitDataRight) mainNormal = new Vector2((hitDataLeft.normal.x+hitDataRight.normal.x)/2, (hitDataLeft.normal.y+hitDataRight.normal.y)/2);
			else if (hitDataLeft) mainNormal = hitDataLeft.normal;
			else mainNormal = hitDataRight.normal;
		}
		else
		{
			isGrounded = false;
			mainNormal = Vector2.up;
		}
		
		// Check sides
		hitDataLeft = Physics2D.Raycast(_t.TransformPoint(_leftRaycastLocalOrigin), Vector2.left, raycastSideDistance, groundLayerMask);
		hitDataRight = Physics2D.Raycast(_t.TransformPoint(_rightRaycastLocalOrigin), Vector2.right, raycastSideDistance, groundLayerMask);
		
		if (hitDataLeft) leftAngle = Vector2.Angle(Vector2.up, hitDataLeft.normal);
		else leftAngle = 0;
		if (hitDataRight) rightAngle = Vector2.Angle(Vector2.up, hitDataRight.normal);
		else rightAngle = 0;
	}
}
