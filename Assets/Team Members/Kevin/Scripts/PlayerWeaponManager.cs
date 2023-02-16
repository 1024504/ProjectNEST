using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public List<GameObject> weaponsList;
    public List<GameObject> bulletList;
    public List<Transform> barrelTransforms;
    public PlayerTest playerTest;

    public void Awake()
    {
        playerTest = GetComponent<PlayerTest>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedWeapon(1);
        }  
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedWeapon(2);
        }
    }

    //Figure out if there is a better more modularised way of doing this.
    //too much ctr c ctr v here. 
    public void SelectedWeapon(int weaponInt)
    {
        switch (weaponInt + 1)
        {
            case 1 :
                playerTest.SetBulletPrefab(bulletList[weaponInt]);
                playerTest.SetBarrelTransform(barrelTransforms[weaponInt]);
                weaponsList[0].SetActive(true);
                weaponsList[1].SetActive(false);
                weaponsList[2].SetActive(false);
                break;
            case 2 :
                playerTest.SetBulletPrefab(bulletList[weaponInt]);
                playerTest.SetBarrelTransform(barrelTransforms[weaponInt]);
                weaponsList[0].SetActive(false);
                weaponsList[1].SetActive(true);
                weaponsList[2].SetActive(false);
                break;
            case 3 :
                playerTest.SetBulletPrefab(bulletList[weaponInt]);
                playerTest.SetBarrelTransform(barrelTransforms[weaponInt]);
                weaponsList[0].SetActive(false);
                weaponsList[1].SetActive(false);
                weaponsList[2].SetActive(true);
                break;
        }
        
    }
}
