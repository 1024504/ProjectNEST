using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float bulletDmg;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        /*WeaponBase weaponBase = GetComponentInParent<WeaponBase>();
        bulletDmg = weaponBase.dmg;*/
        StartCoroutine(ShotRange());
    }
    private IEnumerator ShotRange()
    {
        yield return new WaitForSeconds(1.5f);
        BulletDestroy();
    }

    public void OnTriggerEnter2D(Collider2D colliderObject)
    {
        EnemyTest enemyTest = colliderObject.GetComponent<EnemyTest>();
        if (enemyTest != null)
        {
            Debug.Log("Hit! "+ bulletDmg);
            enemyTest.GotHit(bulletDmg);
            BulletDestroy();
        }
    }
    private void BulletDestroy()
    {
        Destroy(this.GameObject());
    }
}
