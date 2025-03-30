using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource musicSource;
    public static float musicVolume;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musicSource = GetComponent<AudioSource>();
            musicVolume = 0.04f;
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
        musicVolume = PlayerPrefs.GetFloat("musicVolume", musicVolume);
    }
    private void Update()
    {
        //sfxVolume = volumeSlider.value;
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();
    }
}
