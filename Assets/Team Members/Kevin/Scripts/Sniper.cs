using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : WeaponBase
{
   public AudioClip[] sniperSound;
   public AudioClip sniperReload;
   public float reloadTime;
   
   
   #region Shooting

   public override void Shoot()
   {
      if (isReloading) return;
      if(currentMagazine > 0)
      {
         //add rifle sound here
         AudioSource.PlayClipAtPoint(sniperSound[Random.Range(0,3)],transform.position);
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
      isReloading = true;
      AudioSource.PlayClipAtPoint(sniperReload,transform.position);
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
