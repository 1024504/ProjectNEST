using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healthPrefab;
    public PlayerHealth playerHealth;
    public int healthInt;
    private List<HealthCubes> _healths = new List<HealthCubes>();

    public void Start()
    {
        StartCoroutine(DelayDrawHealth());
    }

    public IEnumerator DelayDrawHealth()
    {
        yield return new WaitForSeconds(2f);
        DrawHealth();
    }

    private void OnEnable()
    {
        playerHealth.OnGotHit += DrawHealth;
    }
    
    private void OnDisable()
    {
        playerHealth.OnGotHit -= DrawHealth;
    }
    private void DrawHealth()
    {
        ClearHealth();
        float playerCurrentHp = playerHealth.HealthLevel;
        healthInt = (int) (playerCurrentHp/10);

        for (int i = 0; i < healthInt; i++)
        {
            CreateEmptyHealth();
        }

        for (int i = 0; i < healthInt; i++)
        {
            //int healthStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i*2),0,2);
            _healths[i].SetHealthImage(HealthStatus.Full);
        }
        
    }
    
    private void CreateEmptyHealth()
    {
        GameObject healthGO = Instantiate(healthPrefab);
        healthGO.transform.SetParent(transform);
        
        HealthCubes healthCubesComponent = healthGO.GetComponent<HealthCubes>();
        healthCubesComponent.SetHealthImage(HealthStatus.Empty);
        _healths.Add(healthCubesComponent);

    }
    private void ClearHealth()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        _healths = new List<HealthCubes>();
    }
    
    
}
