using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
[RequireComponent(typeof(Slider))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    public AudioMixer mixer;
    [SerializeField] public string volumeName;
    [SerializeField] TMPro.TMP_Text volumeLabel;
    private void Awake()
    {
        //Keep this object even when we go to new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Destroy duplicate gameobjects
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }
    private void Start()
    {
        UpdateValueOnChange(slider.value);
        slider.onValueChanged.AddListener(delegate { UpdateValueOnChange(slider.value); });
    }
    Slider slider
    {
        get
        {
            return GetComponent<Slider>();
        }
    }
    public void UpdateValueOnChange(float value)
    {
        if(mixer != null)
        {
            mixer.SetFloat(volumeName, Mathf.Log10(value) * 20);
        }
        if(volumeLabel != null)
        {
            volumeLabel.text = Mathf.RoundToInt(value * 100f).ToString() + "%";
        }
    }
}
