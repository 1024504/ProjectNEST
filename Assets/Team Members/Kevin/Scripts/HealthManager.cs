using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : HealthBase
{ 
    public Image[] healthBoxes;

    public Sprite fullHealth;
    public Sprite emptyHealth;
    
    public PlayerHealth playerHealth;

    public void Start()
    {
        healthLevel = maxHealth;
        DrawHealth();
    }
    private void OnEnable()
    {
        playerHealth.OnChangeHealth += DrawHealth;
    }
    
    private void OnDisable()
    {
        playerHealth.OnChangeHealth -= DrawHealth;
    }

    public void DrawHealth()
    {
        healthLevel = (int) playerHealth.HealthLevel/10;
        foreach (Image img in healthBoxes)
        {
            img.sprite = emptyHealth;
        }

        for (int i = 0; i < healthLevel; i++)
        {
            healthBoxes[i].sprite = fullHealth;
        }
    }

}
