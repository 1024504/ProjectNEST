using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rifle : WeaponBase
{
    public float reloadTime;
    public float shotCounter;
    public float rateOfFire = 0.1f;
    public bool canShoot;
    public delegate void OnBulletUpdate();
    public event OnBulletUpdate OnShoot;
    
    private void OnEnable()
    {
        canShoot = true;
        if (currentMagazine <= 0) Reload();
    }
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
        /*else
        {
            shotCounter -= Time.deltaTime;
        }*/
    }
    
    #region Shooting
    
    public override void Shoot()
    {
        if (isReloading) return;
        if(currentMagazine > 0 && isShooting && canShoot)
        {
            Instantiate(bulletPrefab, gunBarrelTransform.position, transform.rotation);
            currentMagazine--;
            OnShoot?.Invoke();
            canShoot = false;
            StartCoroutine(ShotCooldown());
            if(currentMagazine < 1 && isReloading == false)
            {
                StartCoroutine(AutoReloadTimer());
            }
        }
    }
    #endregion

    private IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        canShoot = true;
    }

    #region Reloading

    public override void Reload()
    {
        if (currentMagazine == magazineMax) return;
        if (isReloading) return;
        isReloading = true;
        StartCoroutine(ReloadTimer());
        
    }
    
    private IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadTime);
        currentMagazine = magazineMax;
        OnShoot?.Invoke();
        isReloading = false;
        GameManager.Instance._uiManager.aboveHeadUI.SetActive(false);
    }
    
    private IEnumerator AutoReloadTimer()
    {
        yield return new WaitForSeconds(1f);
        if (isReloading == false)
        {
            Reload();
        }
    }
    
    #endregion
  
}
