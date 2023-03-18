using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCubes : MonoBehaviour
{
    public Sprite full, empty;
    private Image healthImage;
    
    private void Awake()
    {
        healthImage = GetComponent<Image>();
    }

    public void SetHealthImage(HealthStatus status)
    {
        switch (status)
        {
            case HealthStatus.Empty:
                healthImage.sprite = empty;
                break;
            case HealthStatus.Full:
                healthImage.sprite = full;
                break;
        }
    }
}

public enum HealthStatus
{
    Empty = 0,
    Full = 1
}
