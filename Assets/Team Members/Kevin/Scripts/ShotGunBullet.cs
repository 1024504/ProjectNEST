using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : BulletBase
{
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        StartCoroutine(ShotRange());
    }
    private IEnumerator ShotRange()
    {
        yield return new WaitForSeconds(bulletLife);
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
        Destroy(this.gameObject);
    }
}
