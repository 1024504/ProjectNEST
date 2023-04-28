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
    private FMOD.Studio.Bus masterBus;
    private FMOD.Studio.Bus musicBus;
    private FMOD.Studio.Bus sfxBus;
    public float masterVolume = 1;
    public float sfxVolume = 1;
    public float musicVolume = 1;
    
    private void Start()
    {
        GetSettings();
    }

    private void GetSettings()
    {
	    _resolutions = Screen.resolutions;
	    if (resolutionDropdown != null)resolutionDropdown.ClearOptions();
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
        
	    masterVolume = GameManager.Instance.saveData.SettingsData.MasterVolume;
	    musicVolume = GameManager.Instance.saveData.SettingsData.MusicVolume;
	    sfxVolume = GameManager.Instance.saveData.SettingsData.SFXVolume;
        
	    masterBus = FMODUnity.RuntimeManager.GetBus("bus:/Master");
	    musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
	    sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
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
        
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
        
        GameManager.Instance.saveData.SettingsData.MasterVolume = masterVolume;
        GameManager.Instance.saveData.SettingsData.MusicVolume = musicVolume;
        GameManager.Instance.saveData.SettingsData.SFXVolume = sfxVolume;

        GameManager.Instance.SaveGame();
    }

    public void UpdateVolume(float masterVolume, float musicVolume, float sfxVolume)
    {
	    
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
