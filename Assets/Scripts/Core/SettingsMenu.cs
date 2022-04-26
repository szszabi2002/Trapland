using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class SettingsMenu : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private int firstPlayInt;
    public GameObject settings;
    public Slider backgroundSlider, soundEffectsSlider;
    private float backgroundFloat, soundEffectsFloat;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public Button SaveSetting;
    private void Start()
    {
        settings.SetActive(false);
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        if (firstPlayInt == 0)
        {
            backgroundFloat = .125f;
            soundEffectsFloat = .75f;
            backgroundSlider.value = backgroundFloat;
            soundEffectsSlider.value = soundEffectsFloat;
            PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            backgroundSlider.value = backgroundFloat;
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
            soundEffectsSlider.value = soundEffectsFloat;
        }
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
    }
    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }
    public void SaveSettings()
    {
        SaveSoundSettings();
        PlayerPrefs.Save();
    }
}
