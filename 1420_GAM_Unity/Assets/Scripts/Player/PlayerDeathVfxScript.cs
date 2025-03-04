using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerDeathVfxScript : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem deathFeathers;

    private void Start()
    {
        animator = GetComponent<Animator>();
        deathFeathers.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayDeathEffect();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ReverseDeathEffect();
        }
    }

    public void PlayDeathEffect()
    {
        StartCoroutine(DeathEffect());
    }

    public void ReverseDeathEffect()
    {
        deathFeathers.Stop();
        animator.Play("HoleOpenAnim");
    }

    private IEnumerator DeathEffect()
    {
        animator.Play("HoleCloseAnim");
        deathFeathers.Play();
        yield return null;
    }
}
