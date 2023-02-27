using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : WeaponBase
{
   public AudioClip sniperSound;
   public AudioClip sniperReload;
   public float reloadTime;
   
   
   #region Shooting

   public override void Shoot()
   {
      if (isReloading) return;
      if(currentMagazine > 0)
      {
         //add rifle sound here
         AudioSource.PlayClipAtPoint(sniperSound,transform.position);
         Instantiate(bulletPrefab, gunBarrelTransform.position, transform.rotation);
         currentMagazine--;
         if(currentMagazine < 1 && isReloading == false)
         {
            //add reload sound here
            StartCoroutine(AutoReloadTimer());
         }
      }
        
   }

   #endregion
   
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
         AudioSource.PlayClipAtPoint(sniperReload,transform.position);
         Debug.Log("Reloading");
         StartCoroutine(ReloadTimer());
      }
   }
    
   private void AutoReload()
   {
      AudioSource.PlayClipAtPoint(sniperReload,transform.position);
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
