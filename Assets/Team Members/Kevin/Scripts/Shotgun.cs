using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    public Transform[] shotgunBarrelTransforms;
    public AudioClip shotgunSound;
    public AudioClip shotgunReload;
    public float reloadTime;
    public override void Shoot()
    {
        if (isReloading) return;
        if (currentMagazine > 0)
        {
            //add shot gun sound here
            AudioSource.PlayClipAtPoint(shotgunSound, transform.position);
            for (int i = 0; i < shotgunBarrelTransforms.Length; i++)
            {
                Instantiate(bulletPrefab, shotgunBarrelTransforms[i].position, transform.rotation);
            }
            currentMagazine -= 2;
            if(currentMagazine < 1 && isReloading == false)
            {
                //add reload sound here
                StartCoroutine(AutoReloadTimer());
            }
            //Debug.Log("Shotgun: " + dmg);
        }
    }
    
    #region Reloading

    public override void Reload()
    {
        if (currentMagazine == magazineMax) return;
        {
            Debug.Log("Full Clip");
        }
        if (isReloading == false)
        {
            isReloading = true;
            AudioSource.PlayClipAtPoint(shotgunReload,transform.position);
            Debug.Log("Reloading");
            StartCoroutine(ReloadTimer());
        }
    }
    
    private void AutoReload()
    {
        AudioSource.PlayClipAtPoint(shotgunReload,transform.position);
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
        isReloading = true;
        yield return new WaitForSeconds(3f);
        AutoReload();
    }

  

    #endregion
    
}
