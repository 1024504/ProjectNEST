using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Shotgun : WeaponBase
{
    public Transform[] shotgunBarrelTransforms;
    public float reloadTime;
    public bool shotCooldown = true;
    public float shotCooldownTime;

    protected override void OnEnable()
    {
	    base.OnEnable();
        shotCooldown = true;
    }
    public override void Shoot()
    {
        if (isReloading) return;
        if (!shotCooldown) return;
        if (currentMagazine > 0)
        {
            for (int i = 0; i < shotgunBarrelTransforms.Length; i++)
            {
                GameObject go = Instantiate(bulletPrefab, shotgunBarrelTransforms[i].position, transform.rotation);
                Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                rb.velocity = shotgunBarrelTransforms[i].right * 75f;
            }
            currentMagazine --;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Gun SFX/ShotgunSFX/ShotgunShots");
            shotCooldown = false;
            StartCoroutine(ShotCooldownTimer());
            OnShoot?.Invoke();
            if(currentMagazine < 1 && isReloading == false)
            {
                StartCoroutine(AutoReloadTimer());
            }
        }
    }

    private IEnumerator ShotCooldownTimer()
    {
        yield return new WaitForSeconds(shotCooldownTime);
        shotCooldown = true;
    }
    
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
