using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class HealthBase : MonoBehaviour
{
    [SerializeField] [Min(0)] protected float healthLevel;

    [SerializeField] [Min(1f)] protected float maxHealth = 1;
    
    public delegate void UpdateHealth();
    public event UpdateHealth OnChangeHealth;

    public GameObject bloodParticle;
    public EventReference impactSFX;

    public float HealthLevel
    {
        get => healthLevel;
        set
        {
            if (value <= 0) Die();
            else if (value > maxHealth) healthLevel = maxHealth;
            else healthLevel = value;
            OnChangeHealth?.Invoke();
            Instantiate(bloodParticle, transform.position, Quaternion.identity);
            RuntimeManager.PlayOneShot(impactSFX);
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
