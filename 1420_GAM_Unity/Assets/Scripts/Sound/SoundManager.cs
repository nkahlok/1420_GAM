using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

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
    PLAYEREQUIPSWAP,
    PLAYERBLOCKSUCCESS,
    PLAYERBRIEFCASETHROW,
    PLAYERHURT,
    PLAYERHURTSPIKES,
    PLAYERDEATH,
    COMBOUP,
    COMBORAVENOUS,
    ENEMYAGGRO,
    CATWALK,
    CATATTACK,
    CATATTACKSUCCESS,
    CATDEATH,
    RATWALK,
    RATSHOOT,
    RATSHOOTHIT,
    RATRELOAD,
    RATDEATH,
    BOSSDASHATK,
    BOSSFALLATKIMPACT,
    BOSSPROJATK,
    BOSSMULTPROJATK,
    BOSSHURT,
    BOSSDEATH
}
[RequireComponent(typeof(AudioSource))/*, ExecuteInEditMode*/]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    public Slider volumeSlider;
    private static SoundManager instance;
    private AudioSource sfxSource;
    public static float sfxVolume = 0.7f;
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
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        sfxSource.volume = sfxVolume;
        volumeSlider.value = sfxVolume;
    }

    private void Update()
    {
        sfxSource.volume = sfxVolume;
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
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
