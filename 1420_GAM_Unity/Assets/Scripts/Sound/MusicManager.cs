using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource musicSource;
    public static float musicVolume;
    float musicVolumeToHit;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musicSource = GetComponent<AudioSource>();
            musicVolume = 0.05f;
            musicSource.volume = musicVolume;
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        musicVolumeToHit = PlayerPrefs.GetFloat("musicVolume");
        //musicVolume = PlayerPrefs.GetFloat("musicVolume", musicVolume);
        //StartCoroutine(MusicFadeIn());
    }

    //private IEnumerator MusicFadeIn()
    //{
    //    musicVolume = 0f;
    //    while (musicVolume < musicVolumeToHit)
    //    {
    //        musicVolume += Time.deltaTime / 0.7f;
    //    }
    //        yield return null;
    //}



    private void Update()
    {
        //sfxVolume = volumeSlider.value;
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();
    }
}
