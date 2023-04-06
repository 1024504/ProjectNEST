using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : HealthBase
{ 
    public Image[] healthBoxes;

    public Sprite fullHealth;
    public Sprite emptyHealth;
    
    private PlayerHealth _playerHealth;

    public void Start()
    {
        healthLevel = maxHealth;
    }
    private void OnEnable()
    {
	    if (_playerHealth != null) SetPlayer();
    }

    public void SetPlayer()
    {
	    _playerHealth = UIManager.Instance.player.GetComponent<PlayerHealth>();
	    _playerHealth.OnChangeHealth += DrawHealth;
	    DrawHealth();
    }
    
    private void OnDisable()
    {
        _playerHealth.OnChangeHealth -= DrawHealth;
    }

    public void DrawHealth()
    {
        healthLevel = (int) _playerHealth.HealthLevel/10f;
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
