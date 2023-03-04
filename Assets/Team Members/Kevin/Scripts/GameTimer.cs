using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float survivalTime;

    public void Awake()
    {
        survivalTime = 0;
    }
    public void Update()
    {
        survivalTime += Time.deltaTime;
        timerText.text = survivalTime.ToString("F1");
    }
}
