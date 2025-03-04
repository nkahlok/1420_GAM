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
        animator.Play("HoleCloseAnim");
        deathFeathers.Play();
    }

    public void ReverseDeathEffect()
    {
        deathFeathers.Stop();
        animator.Play("HoleOpenAnim");
    }

}
