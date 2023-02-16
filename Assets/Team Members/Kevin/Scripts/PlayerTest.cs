using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrelTransform;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity);
        }
    }
    public void SetBarrelTransform(Transform newTransform)
    {
        barrelTransform = newTransform;
    }
    public void SetBulletPrefab(GameObject newBullet)
    {
        bulletPrefab = newBullet;
    }
}
