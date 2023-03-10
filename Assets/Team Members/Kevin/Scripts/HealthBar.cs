using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healthPrefab;
    public PlayerHealth playerHealth;
    public int healthInt;
    private List<Health> _healths = new List<Health>();

    public void Start()
    {
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
        float playerCurrentHp = playerHealth.health;
        healthInt = (int) (playerCurrentHp/10);

        for (int i = 0; i < healthInt; i++)
        {
            CreateEmptyHealth();
        }

        for (int i = 0; i < _healths.Count; i++)
        {
            int healthStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i*2),0,2);
            _healths[i].SetHealthImage((HealthStatus)healthStatusRemainder);
        }
        
    }
    
    private void CreateEmptyHealth()
    {
        GameObject healthGO = Instantiate(healthPrefab);
        healthGO.transform.SetParent(transform);
        
        Health healthComponent = healthGO.GetComponent<Health>();
        healthComponent.SetHealthImage(HealthStatus.Full);
        _healths.Add(healthComponent);

    }
    private void ClearHealth()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        _healths = new List<Health>();
    }
    
    
}
