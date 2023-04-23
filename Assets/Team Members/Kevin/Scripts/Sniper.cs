using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : WeaponBase
{
   public float reloadTime;
   
   #region Shooting

   public override void Shoot()
   {
      if (isReloading) return;
      if(currentMagazine > 0)
      {
         Instantiate(bulletPrefab, gunBarrelTransform.position, transform.rotation);
         currentMagazine--;
         FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Gun SFX/SniperSFX/SniperShots");
         OnShoot?.Invoke();
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
      FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Gun SFX/ReloadSFX/Reload");  
      StartCoroutine(ReloadTimer());
        
   }
    
   private IEnumerator ReloadTimer()
   {
      yield return new WaitForSeconds(reloadTime);
      currentMagazine = magazineMax;
      OnShoot?.Invoke();
      isReloading = false;
      GameManager.Instance.uiManager.aboveHeadUI.SetActive(false);
   }
    
   private IEnumerator AutoReloadTimer()
   {
      yield return new WaitForSeconds(1f);
      if (isReloading == false)
      {
         GameManager.Instance.uiManager.aboveHeadUI.SetActive(true);
         Reload();
      }
   }

  

   #endregion
}
