using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthBase
{
	public Transform respawnPoint;
	public UIManager uiManager;
	public GameTimer gameTimer;
	protected override void Die()
	{
		/*GameObject go = gameObject.GetComponentInParent<Player>().gameObject;
		go.transform.position = respawnPoint.position;
		HealthLevel = maxHealth;
		GameManager.Instance.survivalTimer = 0f;
		GameManager.Instance.killCount = 0;*/
		uiManager.survivalTimeText.text = gameTimer.survivalTime.ToString("F1");
		uiManager.Pause();
	}
}
