using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum SoundType //list of sounds, e.g. to use SoundManager.PlaySfx(SoundType.PLAYERWALK);
{
    PLAYERWALK,
    PLAYERATTACK,
    PLAYERATTACKSUCCESS,
    PLAYERJUMP,
    PLAYERFEATHERS,
    PLAYERDASH,
    PLAYERPARRY,
    PLAYERUPPERCUT,
    PLAYERPARRYSUCCESS,
    PLAYERHURT,
    PLAYERHURTSPIKES,
    PLAYERDEATH,
    COMBOUP,
    COMBORAVENOUS,
    CATWALK,
    CATATTACK,
    CATATTACKSUCCESS,
    CATHURT,
    CATDEATH,
    RATWALK,
    RATSHOOT,
    RATSHOOTHIT,
    RATHURT,
    RATDEATH
}
[RequireComponent(typeof(AudioSource))/*, ExecuteInEditMode*/]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource sfxSource;
    public static float sfxVolume = 1.0f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            sfxSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        //look into playerprefs for saveable volume if you have time.
        sfxSource.volume = sfxVolume;
    }
    
    public static void PlaySfx(SoundType sound)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.sfxSource.PlayOneShot(randomClip, sfxVolume);
        //instance.sfxSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    //public static void PlayMusic(SoundType sound, float volume = 1)
    //{
    //    instance.sfxSource.PlayOneShot(instance.soundList[(int)sound], volume);
    //}

//#if UNITY_EDITOR
//    private void OnEnable()
//    {
//        string[] names = Enum.GetNames(typeof(SoundType));
//        Array.Resize(ref soundList, names.Length);
//        for (int i = 0; i < soundList.Length; i++)
//        {
//            soundList[i].name = names[i];
//        }
//    }
//#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds;}
    [SerializeField] private string name;
    [SerializeField] private AudioClip[] sounds;
}
