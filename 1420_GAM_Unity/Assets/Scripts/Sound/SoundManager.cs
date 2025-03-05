using System;
using UnityEngine;

public enum SoundType
{
    BGM,
    PLAYERWALK,
    PLAYERATTACK,
    PLAYERATTACKSUC,
    PLAYERJUMP,
    PLAYERDASH,
    PLAYERLUNGE,
    PLAYERUPPERCUT,
    PLAYERHURT,
    PLAYERDEATH
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource sfxSource;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlaySfx(SoundType.BGM);
    }
    
    public static void PlaySfx(SoundType sound, float volume = 1)
    {
        instance.sfxSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    public static void PlayMusic(SoundType sound, float volume = 1)
    {
        instance.sfxSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

}
