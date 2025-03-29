using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public GameObject sfxManager;
    private SoundManager sfxManagerInstance;
    public AudioSource musicManagerInstance;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        PlayerPrefs.SetFloat("musicVolume", 0.04f);
    }
    void Start()
    {
        //sfxSlider = GetComponent<Slider>();
        //musicSlider = GetComponent<Slider>();
        sfxManager = GameObject.FindWithTag("SoundManager");
        sfxManagerInstance = sfxManager.GetComponent<SoundManager>();
        if (PlayerPrefs.HasKey("sfxVolume") && PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSfxVolume();
            SetMusicVolume();
        }
    }

    public void SetSfxVolume()
    {
        float sfxVolume = sfxSlider.value;
        SoundManager.sfxVolume = sfxVolume;
    }
    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        musicManagerInstance.volume = musicVolume;
        PlayerPrefs.SetFloat("musicVolume",musicVolume);
    }

    public void LoadVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetSfxVolume();
        SetMusicVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
