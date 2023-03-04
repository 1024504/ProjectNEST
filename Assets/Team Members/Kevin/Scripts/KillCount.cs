using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    public TextMeshProUGUI killcountText;
    public int killCount;
    public void Awake()
    {
        killCount = 0;
    }

    public void Update()
    {
        killcountText.text = killCount.ToString();
    }
}
