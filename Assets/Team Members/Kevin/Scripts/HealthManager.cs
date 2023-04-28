using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{ 
    public Image[] healthBoxes;

    public Sprite fullHealth;
    public Sprite emptyHealth;
    
    private PlayerHealth _playerHealth;
    
    private void OnEnable()
    {
	    if (_playerHealth != null) SetPlayer();
	    GameManager.Instance.OnFinishLoading += DrawHealth;
    }

    public void SetPlayer()
    {
	    _playerHealth = UIManager.Instance.player.GetComponent<PlayerHealth>();
	    _playerHealth.OnChangeHealth += DrawHealth;
	    DrawHealth();
    }
    
    private void OnDisable()
    {
        if (_playerHealth != null) _playerHealth.OnChangeHealth -= DrawHealth;
    }

    public void DrawHealth()
    {
        int healthBlocks = (int) (_playerHealth.HealthLevel/_playerHealth.maxHealth*10f);
        foreach (Image img in healthBoxes)
        {
            img.sprite = emptyHealth;
        }

        for (int i = 0; i < healthBlocks; i++)
        {
            healthBoxes[i].sprite = fullHealth;
        }
    }

}
