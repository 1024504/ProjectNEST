using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
	public LayerMask hitBoxLayer;
    private EnemyBody _enemy;
    private Transform _transform;
    private Player _player;
    [SerializeField]
    private Transform viewTransform;
    private SpriteRenderer _viewRenderer;
    private Vector3 _viewLocalScale;
    
    public void OnEnable()
    {
	    _transform = transform;
	    _player = GetComponentInParent<Player>();
	    _viewRenderer = viewTransform.GetComponent<SpriteRenderer>();
	    _viewLocalScale = viewTransform.localScale;
    }
    
    private void UnselectTarget()
    {
	    if (_enemy != null) _enemy.GetComponent<HealthBase>().OnDeath -= UnselectTarget;
	    _enemy = null;
	    _viewRenderer.color = Color.cyan;
    }

    public void Update()
    {
	    RaycastHit2D closestValidHit = new RaycastHit2D();
	    Vector3 barrelPosition = _player.currentWeapon.gunBarrelTransform.position;
	    RaycastHit2D[] hits = Physics2D.RaycastAll(barrelPosition, _transform.position-barrelPosition, _player.currentWeapon.bulletRange, hitBoxLayer);

	    foreach (RaycastHit2D hit in hits)
	    {
		    if (!hit.transform.IsChildOf(_player.gameObject.transform) &&
		        (closestValidHit.collider == null || closestValidHit.distance > hit.distance))
		    {
			    closestValidHit = hit;
		    }
	    }

	    LaserPointer laserPointer = _player.currentWeapon.laserPointer;
	    if (closestValidHit.collider != null)
	    {
		    laserPointer.NewSize(closestValidHit.point);
		    EnemyBody enemy = closestValidHit.collider.GetComponentInParent<EnemyBody>();
		    if (enemy != null && enemy != _enemy)
		    {
			    _viewRenderer.color = Color.red;
			    if (_enemy != null) _enemy.GetComponent<HealthBase>().OnDeath -= UnselectTarget;
			    _enemy = enemy;
			    _enemy.GetComponent<HealthBase>().OnDeath += UnselectTarget;
		    }
		    else
		    {
			    UnselectTarget();
		    }
	    }
	    else
	    {
		    laserPointer.DefaultSize();
		    UnselectTarget();
	    }
	    
        if (_enemy != null)
        {
	        viewTransform.rotation = Quaternion.RotateTowards(viewTransform.rotation, Quaternion.Euler(0, 0, -45f), 10f);
	        viewTransform.localScale = _viewLocalScale * 2;
        }
        else
        {
	        viewTransform.localPosition = Vector3.zero;
	        viewTransform.rotation = Quaternion.RotateTowards(viewTransform.rotation,Quaternion.Euler(0, 0, 0) , 10f);
	        viewTransform.localScale = _viewLocalScale;
        }
    }
}
