using UnityEngine;

public class CatFootStep : MonoBehaviour
{
    AudioSource m_AudioSource;
    public AudioClip catWalk;
    private void Start()
    {
        m_AudioSource = GetComponentInParent<AudioSource>();
    }
    public void PlaySound()
    {
        m_AudioSource.PlayOneShot(catWalk, 0.3f);
    }
}
