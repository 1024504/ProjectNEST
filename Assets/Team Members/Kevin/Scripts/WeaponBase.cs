using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponBase : MonoBehaviour, IShootable
{
    public float dmg;
    public int currentMagazine;
    public int magazineMax;
    public float shotRange;
    public Transform gunBarrelTransform;
    public GameObject bulletPrefab;
    public bool isReloading;


    public virtual void Shoot()
    {
        
    }

    
    public virtual void Reload()
    {
        
    }
    
}
