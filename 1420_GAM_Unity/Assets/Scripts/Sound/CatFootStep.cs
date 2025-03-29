using UnityEngine;

public class CatFootStep : MonoBehaviour
{
    AudioSource m_AudioSource;
    public AudioClip catWalk;
    public static bool playerSFXTriggerCollided = false;
    private void Start()
    {
        m_AudioSource = GetComponentInParent<AudioSource>();
    }
    public void PlaySound()
    {
        if (playerSFXTriggerCollided == true)
        {
            Debug.Log("footstep play");
            m_AudioSource.PlayOneShot(catWalk, 0.3f);
        }
    }
}
