using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [SerializeField] [Min(0)] protected float healthLevel;

    [SerializeField] [Min(1f)] protected float maxHealth = 1;
    
    public delegate void UpdateHealth();
    public event UpdateHealth OnChangeHealth;
    
    public float HealthLevel
    {
        get => healthLevel;
        set
        {
            if (value <= 0) Die();
            else if (value > maxHealth) healthLevel = maxHealth;
            else healthLevel = value;
            OnChangeHealth?.Invoke();
        }
    }


    private void Awake()
    {
        healthLevel = maxHealth;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
