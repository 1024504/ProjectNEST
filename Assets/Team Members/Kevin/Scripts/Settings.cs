using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Settings : MonoBehaviour
{
    [Header("Screen Resolution")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown screenModeDropDown;
    [SerializeField] private TMP_Dropdown graphicsDropDown;
    private Resolution[] _resolutions;
    private List<Resolution> resolutions;
    private int currentResolutionIndex = 0;
    private int currentScreenModeIndex;
    private int currentQualityIndex;
    
    void Start()
    {
        _resolutions = Screen.resolutions;
        resolutions = new List<Resolution>();
        resolutionDropdown.ClearOptions();
        for (int i = 0; i < _resolutions.Length; i++)
        {
            resolutions.Add(_resolutions[i]);
        }
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Count; i++)
        {
            string resolutionOption = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(resolutionOption);
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void DropdownValue()
    {
        currentResolutionIndex =  resolutionDropdown.value;
        Debug.Log(currentResolutionIndex);
    }

    public void ScreenModeDropdown()
    {
        currentScreenModeIndex = screenModeDropDown.value;
    }
    
    public void ApplyChanges()
    {
        Resolution resolution = resolutions[currentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        QualitySettings.SetQualityLevel(currentQualityIndex);
        Debug.Log(resolution.width + "x" + resolution.height + QualitySettings.names[currentQualityIndex]);
    }

    public void SetQualityIndex()
    {
        currentQualityIndex = graphicsDropDown.value;
        Debug.Log(currentQualityIndex);
    }
}
