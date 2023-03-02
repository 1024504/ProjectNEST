using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rifle : WeaponBase
{
    public List<AudioClip> rifleSound;
    public AudioClip rifleReload;
    public float reloadTime;
    public float shotCounter;
    public float rateOfFire = 0.1f;
    public void Update()
    {
        if (isShooting)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = rateOfFire;
                Shoot();
            }
        }
        else
        {
            shotCounter -= Time.deltaTime;
        }
    }
    
    #region Shooting
    
    public override void Shoot()
    {
        if (isReloading) return;
        if(currentMagazine > 0)
        {
            //add rifle sound here
            AudioSource.PlayClipAtPoint(rifleSound[Random.Range(0,3)],transform.position);
            Instantiate(bulletPrefab, gunBarrelTransform.position, transform.rotation);
            currentMagazine--;
            if(currentMagazine < 1 && isReloading == false)
            {
                StartCoroutine(AutoReloadTimer());
            }
        }
        
    }

    #endregion


    #region Reloading

    public override void Reload()
    {
        if (currentMagazine == magazineMax) return;
        if (isReloading) return;
        
        if (isReloading == false)
        {
            isReloading = true;
            AudioSource.PlayClipAtPoint(rifleReload,transform.position);
            StartCoroutine(ReloadTimer());
        }
    }
    
    private void AutoReload()
    {
        AudioSource.PlayClipAtPoint(rifleReload,transform.position);
        Debug.Log("AutoReloading");
        StartCoroutine(ReloadTimer());
    }
    private IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadTime);
        currentMagazine = magazineMax;
        isReloading = false;
    }
    
    private IEnumerator AutoReloadTimer()
    {
        yield return new WaitForSeconds(3f);
        if (isReloading)
        {
            Debug.Log("reloading already!");
        }
        else
        {
            AutoReload();
        }
        
    }

  

    #endregion
  
}
