using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;


public class MusicManager : MonoBehaviour
{
    //[SerializeField] private MusicList[] musicList;
    public MusicTest[] musicSounds;
    public static MusicManager instance;
    private AudioSource musicSource;
    public static float musicVolume;
    float musicVolumeToHit;
    public static int nowPlayingNumber;
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
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        //musicVolume = PlayerPrefs.GetFloat("musicVolume", musicVolume);
        //StartCoroutine(MusicFadeIn());
    }

    //public static void PlayMusic(MusicType music)
    //{
    //    AudioClip[] clips = instance.musicList[(int)music].Music;
    //    AudioClip nowPlaying = clips[nowPlayingNumber];
    //    instance.musicSource.Play(nowPlaying);
    //}
    //private IEnumerator MusicFadeIn()
    //{
    //    musicVolume = 0f;
    //    while (musicVolume < musicVolumeToHit)
    //    {
    //        musicVolume += Time.deltaTime / 0.7f;
    //    }
    //        yield return null;
    //}
    public void PlayMusic(string name)
    {
        MusicTest m = Array.Find(musicSounds, x => x.name == name);

        if (m == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = m.clip;
            musicSource.Play();
        }
    }


    private void Update()
    {
        //sfxVolume = volumeSlider.value;
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();
    }

}

//[Serializable]
//public struct MusicList
//{
//    public AudioClip[] Music { get => music; }
//    [SerializeField] private string name;
//    [SerializeField] private AudioClip[] music;
//}


