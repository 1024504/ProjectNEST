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
        rb.AddForce(Vector3.right * speed);
        StartCoroutine(ShotRange());
    }
    private IEnumerator ShotRange()
    {
        yield return new WaitForSeconds(3f);
        BulletDestroy();
    }

    public void OnTriggerEnter2D(Collider2D colliderObject)
    {
        EnemyTest enemyTest = colliderObject.GetComponent<EnemyTest>();
        if (enemyTest != null)
        {
            Debug.Log("Hit!");
            enemyTest.GotHit(bulletDmg);
            BulletDestroy();
        }
    }
    private void BulletDestroy()
    {
        Destroy(this.GameObject());
    }
}
