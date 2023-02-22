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

    public void Shoot()
    {
        if (currentMagazine > 0)
        {
            if (isShotgun)
            {
                //add shot gun sound here
                AudioSource.PlayClipAtPoint(gunShot,transform.position);
                Instantiate(bulletPrefab, gunBarrelTransform.position, transform.rotation);
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
            AudioSource.PlayClipAtPoint(reload,transform.position);
            isReloading = true;
        }
       
    }

    public void Reload()
    {
        StartCoroutine(ReloadTimer());
    }

    IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadTime);
        currentMagazine = magazineMax;
        isReloading = false;
    }
}
