using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    public Transform[] shotgunBarrelTransforms;
    public float reloadTime;
    
    public delegate void OnBulletUpdate();
    public event OnBulletUpdate OnShoot;
    public override void Shoot()
    {
        if (isReloading) return;
        if (currentMagazine > 0)
        {
            for (int i = 0; i < shotgunBarrelTransforms.Length; i++)
            {
                Instantiate(bulletPrefab, shotgunBarrelTransforms[i].position, transform.rotation);
            }
            currentMagazine -= 2;
            OnShoot?.Invoke();
            if(currentMagazine < 1 && isReloading == false)
            {
                StartCoroutine(AutoReloadTimer());
            }
        }
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
