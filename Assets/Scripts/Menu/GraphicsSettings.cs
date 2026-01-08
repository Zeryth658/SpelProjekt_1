using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GraphicsSettings : MonoBehaviour
{
    
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown windowModeDropdown;
    public TMP_Dropdown fpsDropdown;
    public Toggle vSyncToggle;
    
    private Resolution[] _resolutions;
    void Start()
    {
        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height + "@" + _resolutions[i].refreshRateRatio + "Hz";
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
        windowModeDropdown.ClearOptions();
        windowModeDropdown.AddOptions(new List<string>()
        {
            "Fullscreen",
            "Borderless",
            "Windowed"
        });
        
        fpsDropdown.ClearOptions();
        List<string> fpsOptions = new List<string>() {"Unlimited", "30", "60", "120", "165"};
        fpsDropdown.AddOptions(fpsOptions);
        
        LoadSettings();
    }
    
    private void LoadSettings()
    {
        if (!PlayerPrefs.HasKey("Resolution")) return; 
        
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        windowModeDropdown.value = PlayerPrefs.GetInt("WindowMode");
        vSyncToggle.isOn = PlayerPrefs.GetInt("VSync") == 1;
        fpsDropdown.value = PlayerPrefs.GetInt("FPS");
        ApplySettings();
    }

    public void ApplySettings()
    {
        Resolution resolution = _resolutions[resolutionDropdown.value];

        FullScreenMode screenMode = FullScreenMode.Windowed;
        switch (windowModeDropdown.value)
        {
            case 0: screenMode = FullScreenMode.ExclusiveFullScreen; break;
            case 1: screenMode = FullScreenMode.FullScreenWindow; break;
            case 2: screenMode = FullScreenMode.Windowed; break;
        }
        Screen.SetResolution(resolution.width, resolution.height, screenMode);
        QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;
        
        string selectedFPS = fpsDropdown.options[fpsDropdown.value].text;
        if (selectedFPS == "Unlimited")
            Application.targetFrameRate = -1;
        else
            Application.targetFrameRate = int.Parse(selectedFPS);
        
        SaveSettings();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("WindowMode", windowModeDropdown.value);
        PlayerPrefs.SetInt("VSync", vSyncToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("FPS", fpsDropdown.value); 
        PlayerPrefs.Save();
    }


}
