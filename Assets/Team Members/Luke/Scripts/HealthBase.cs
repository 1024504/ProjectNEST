using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class HealthBase : MonoBehaviour
{
    [SerializeField] [Min(0)] protected float healthLevel;

    [SerializeField] [Min(1f)] public float maxHealth = 1;
    
    public delegate void UpdateHealth();
    public event UpdateHealth OnChangeHealth;
    public Action OnDeath;

    public GameObject bloodParticle;
    public GameObject deathParticle;
    
    public EventReference impactSFX;
    public float HealthLevel
    {
        get => healthLevel;
        set
        {
            if (value < healthLevel)
            {
                if (bloodParticle != null) Instantiate(bloodParticle, transform.position, Quaternion.identity);
                if (!impactSFX.IsNull) RuntimeManager.PlayOneShot(impactSFX);
            }
            healthLevel = Mathf.Min(value, maxHealth);
            OnChangeHealth?.Invoke();
            if (value <= 0) Die();
        }
    }

    private void Awake()
    {
        healthLevel = maxHealth;
    }

    protected virtual void Die()
    {
	    OnDeath?.Invoke();
        if (deathParticle != null) Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
