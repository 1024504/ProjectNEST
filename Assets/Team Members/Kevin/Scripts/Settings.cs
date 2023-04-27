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
    private int currentResolutionIndex = 0;
    private int currentScreenModeIndex;
    private int currentQualityIndex;
    
    void Start()
    {
        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string resolutionOption = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(resolutionOption);
            if (_resolutions[i].height == Screen.currentResolution.height && _resolutions[i].width == Screen.currentResolution.width) currentResolutionIndex = i;
        }
        if (Screen.fullScreen) currentScreenModeIndex = 0;
		else currentScreenModeIndex = 1;
        
        // Resolution
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
        // Screen Mode
        screenModeDropDown.value = currentScreenModeIndex;
        screenModeDropDown.RefreshShownValue();
        
        // Graphics Quality
        currentQualityIndex = QualitySettings.GetQualityLevel();
        graphicsDropDown.value = currentQualityIndex;
        graphicsDropDown.RefreshShownValue();
    }

    public void ResolutionDropdown()
    {
        currentResolutionIndex =  resolutionDropdown.value;
    }

    public void ScreenModeDropdown()
    {
        currentScreenModeIndex = screenModeDropDown.value;
    }
    
    public void ApplyChanges()
    {
        Resolution resolution = _resolutions[currentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        QualitySettings.SetQualityLevel(currentQualityIndex);
        
        GameManager.Instance.saveData.SettingsData.Resolution = resolution;
        GameManager.Instance.saveData.SettingsData.Fullscreen = Screen.fullScreen;
        GameManager.Instance.saveData.SettingsData.Quality = currentQualityIndex;
        
        GameManager.Instance.SaveGame();
    }
    
    public void ApplyChanges(SettingsData settingsData)
    {
		Resolution resolution = settingsData.Resolution;
		Screen.SetResolution(resolution.width, resolution.height, settingsData.Fullscreen);
		QualitySettings.SetQualityLevel(settingsData.Quality);
	}

    public void GraphicsDropdown()
    {
        currentQualityIndex = graphicsDropDown.value;
    }
}
