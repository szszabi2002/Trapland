 using UnityEngine;
 using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    public AudioClip[] soundtrack;
     private int i;
    private void Awake()
    {
        source = GetComponent<AudioSource>();

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
    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    void Start()
    {
        i = Random.Range(0, soundtrack.Length);
        source.clip = soundtrack[i];
        source.Play();
    }
}
