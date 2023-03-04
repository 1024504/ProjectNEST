using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    public Transform[] shotgunBarrelTransforms;
    public AudioClip[] shotgunSound;
    public AudioClip shotgunReload;
    public float reloadTime;
    public override void Shoot()
    {
        if (isReloading) return;
        if (currentMagazine > 0)
        {
            AudioSource.PlayClipAtPoint(shotgunSound[Random.Range(0,3)], transform.position);
            for (int i = 0; i < shotgunBarrelTransforms.Length; i++)
            {
                Instantiate(bulletPrefab, shotgunBarrelTransforms[i].position, transform.rotation);
            }
            currentMagazine -= 2;
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
        AudioSource.PlayClipAtPoint(shotgunReload,transform.position);
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
        yield return new WaitForSeconds(1f);
        if (isReloading == false)
        {
            Reload();
        }
    }
    #endregion
    
}
