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
    public GameObject bulletPrefab;
    public bool isReloading;
    public bool isShooting;
    public float cameraSize;

    public virtual void Shoot()
    {
        
    }

    
    public virtual void Reload()
    {
        
    }
    
}
