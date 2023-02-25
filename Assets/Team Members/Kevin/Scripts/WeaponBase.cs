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
    public float reloadTime;
    public Transform gunBarrelTransform;
    public GameObject bulletPrefab;
    public bool isShotgun;
    public bool isReloading;
    public AudioClip gunShot;
    public AudioClip reload;

    public bool isRifle;
    public bool isShootingRifle;
    public bool isNotShooting;
    
    //shot gun spread test
    public Transform[] shotgunBarrelTransforms;

    public void Update()
    {
        /*if (isRifle && isShootingRifle)
        {
            Shoot();
        }*/
    }
    public void Shoot()
    {
        if (isReloading) return;
        if (currentMagazine > 0)
        {
            if (isShotgun)
            {
                //add shot gun sound here
                AudioSource.PlayClipAtPoint(gunShot,transform.position);
                for (int i = 0; i < shotgunBarrelTransforms.Length; i++)
                {
                    Instantiate(bulletPrefab, shotgunBarrelTransforms[i].position, transform.rotation);
                }
                currentMagazine-= 2;
                Debug.Log("Shotgun: " + dmg);
            }
            else
            {
                //add rifle sound here
                AudioSource.PlayClipAtPoint(gunShot,transform.position);
                Instantiate(bulletPrefab, gunBarrelTransform.position, transform.rotation);
                currentMagazine--;
                Debug.Log(dmg); 
            }
        }
        else if(isReloading == false)
        {
            //add reload sound here
            //Or out of ammo UI
            Reload();
        }
       
    }
    public void Reload()
    {
        if (currentMagazine == magazineMax)
        {
            Debug.Log("Full Clip");
            return;
        }

        if (isReloading == false)
        {
            isReloading = true;
            AudioSource.PlayClipAtPoint(reload,transform.position);
            Debug.Log("Reloading");
            StartCoroutine(ReloadTimer());
        }
    }

    IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadTime);
        currentMagazine = magazineMax;
        isReloading = false;
    }
}
