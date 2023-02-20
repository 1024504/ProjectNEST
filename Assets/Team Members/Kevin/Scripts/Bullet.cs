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
    public Vector3 mousePos;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed/10f;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        //rb.AddForce(transform.right * speed);
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
