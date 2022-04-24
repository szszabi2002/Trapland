using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance { get; private set; }
    private AudioSource source;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
