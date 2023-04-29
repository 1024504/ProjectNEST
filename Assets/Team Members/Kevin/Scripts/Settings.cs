using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Settings : MonoBehaviour
{
	[Header("General Settings")]
	private bool toggleSubtitles;
	private bool toggleSprint;
	private bool toggleHUD;
	[SerializeField] private Toggle subtitlesToggle;
	[SerializeField] private Toggle sprintToggle;
	[SerializeField] private Toggle hudToggle;
    [Header("Screen Settings")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown screenModeDropDown;
    [SerializeField] private TMP_Dropdown graphicsDropDown;
    private Resolution[] _resolutions;
    private int currentResolutionIndex = 0;
    private int currentScreenModeIndex;
    private int currentQualityIndex;
    [Header("Audio Settings")]
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
	    SettingsData settings = GameManager.Instance.saveData.SettingsData;
	    toggleSubtitles = settings.ToggleSubtitles;
	    toggleSprint = settings.ToggleSprint;
	    toggleHUD = settings.ToggleHUD;
	    subtitlesToggle.isOn = toggleSubtitles;
	    sprintToggle.isOn = toggleSprint;
	    hudToggle.isOn = toggleHUD;

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
        
	    masterVolume = settings.MasterVolume;
	    musicVolume = settings.MusicVolume;
	    sfxVolume = settings.SFXVolume;
        
	    masterBus = FMODUnity.RuntimeManager.GetBus("bus:/Master");
	    musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
	    sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
    }
    
    public void ToggleSubtitles()
	{
	    toggleSubtitles = subtitlesToggle.isOn;
	}
    
    public void ToggleSprint()
	{
	    toggleSprint = sprintToggle.isOn;
	}
    
    public void ToggleHUD()
    {
	    toggleHUD = hudToggle.isOn;
	}

    public void ResolutionDropdown()
    {
        currentResolutionIndex =  resolutionDropdown.value;
    }

    public void ScreenModeDropdown()
    {
        currentScreenModeIndex = screenModeDropDown.value;
    }

    public void GraphicsDropdown()
    {
	    currentQualityIndex = graphicsDropDown.value;
    }
    
    public void ApplyChangesAndSave()
    {
	    GameManager gm = GameManager.Instance;
	    
	    gm.saveData.SettingsData.ToggleSubtitles = toggleSubtitles;
	    gm.saveData.SettingsData.ToggleSprint = toggleSprint;
	    gm.saveData.SettingsData.ToggleHUD = toggleHUD;
	    
	    gm.saveData.SettingsData.Resolution = currentResolutionIndex;
	    if (currentScreenModeIndex == 0) gm.saveData.SettingsData.Fullscreen = true;
	    else gm.saveData.SettingsData.Fullscreen = false;
	    gm.saveData.SettingsData.Quality = currentQualityIndex;
        
        gm.saveData.SettingsData.MasterVolume = masterVolume;
        gm.saveData.SettingsData.MusicVolume = musicVolume;
        gm.saveData.SettingsData.SFXVolume = sfxVolume;
        
        if (gm.playerController.GameplayAgent != null) gm.uiManager.ToggleHUD();
        
        ApplyChanges();

        gm.SaveGame();
    }
    
    public void ApplyChanges()
    {
	    SettingsData settingsData = GameManager.Instance.saveData.SettingsData;
	    int c = 0;
	    for (int i = 0; i < Screen.resolutions.Length; i++)
	    {
		    if (settingsData.Resolution >= Screen.resolutions.Length) break;
		    if (Screen.resolutions[i].width != Screen.resolutions[settingsData.Resolution].width &&
		        Screen.resolutions[i].height != Screen.resolutions[settingsData.Resolution].height) continue;
		    currentResolutionIndex = i;
		    c++;
		    break;
	    }
	    if (c == 0)
	    {
		    Debug.Log("Resolution not found");
		    currentResolutionIndex = Screen.resolutions.Length - 1;
	    }
	    Resolution resolution = Screen.resolutions[currentResolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, settingsData.Fullscreen);
		QualitySettings.SetQualityLevel(settingsData.Quality);
		
		masterBus.setVolume(settingsData.MasterVolume);
		musicBus.setVolume(settingsData.MusicVolume);
		sfxBus.setVolume(settingsData.SFXVolume);
    }
}
