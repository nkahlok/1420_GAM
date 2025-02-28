using UnityEngine;

public enum SoundType
{
    WALK,
    ATTACK,
    JUMP,
    UPPERCUT,
    HURT,
    DEATH
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {

    }
}
