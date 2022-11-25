using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] [Min(0)] private float healthLevel;

    [SerializeField] [Min(1f)] private float maxHealth = 1;

    public float HealthLevel
    {
        get => healthLevel;
        set
        {
            if (value < 0) Die();
            else if (value > maxHealth) healthLevel = maxHealth;
            else healthLevel = value;
        }
    }


    private void Start()
    {
        healthLevel = maxHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
