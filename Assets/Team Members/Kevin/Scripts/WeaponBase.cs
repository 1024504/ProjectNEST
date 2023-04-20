using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponBase : MonoBehaviour, IShootable
{
    public int currentMagazine;
    public int magazineMax;
    public Transform gunBarrelTransform;
    public LaserPointer laserPointer;
    public GameObject bulletPrefab;
    public bool isReloading;
    public bool isShooting;
    public float cameraSize;
    [HideInInspector]
    public float bulletRange;

    protected virtual void OnEnable()
    {
	    BulletBase bulletBase = bulletPrefab.GetComponent<BulletBase>();
	    bulletRange = bulletBase.speed * bulletBase.bulletLife;
	    if (currentMagazine <= 0) Reload();
    }

    public virtual void Shoot()
    {
        
    }

    
    public virtual void Reload()
    {
        
    }
    
}
