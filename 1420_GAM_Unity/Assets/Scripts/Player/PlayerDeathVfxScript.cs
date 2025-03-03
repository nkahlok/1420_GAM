using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerDeathVfxScript : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private Material pDeathEffectMaterial;
    [SerializeField] private float powerStartAmt = 15f;
    [SerializeField] private float powerEndAmt = 0f;
    [SerializeField] private float intensityStartAmt = 1f;
    [SerializeField] private float intensityEndAmt = 1f;
    private int _intensity = Shader.PropertyToID("_VignetteIntensity");
    private int _power = Shader.PropertyToID("_VignettePower");
    private void Start()
    {
        pDeathEffectMaterial.SetFloat(_intensity, intensityStartAmt);
        pDeathEffectMaterial.SetFloat(_power, powerStartAmt);
    }

    public void PlayDeathEffect()
    {
        StartCoroutine(PlayFullscreenEffect());
    }

    public void ReverseDeathEffect()
    {
        StartCoroutine(ReverseFullscreenEffect());
    }

    private IEnumerator PlayFullscreenEffect()
    {
        pDeathEffectMaterial.SetFloat(_intensity, 10f);
        pDeathEffectMaterial.SetFloat(_power, 0f);
        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            //float lerpedIntensity = Mathf.Lerp(, (elapsedTime / _duration));
            float lerpedPower = Mathf.Lerp(powerStartAmt, powerEndAmt, (elapsedTime / _duration));
            pDeathEffectMaterial.SetFloat(_power, lerpedPower);
            //pDeathEffectMaterial.SetFloat(_intensity, lerpedIntensity);
            yield return null;
        }
    }

    private IEnumerator ReverseFullscreenEffect()
    {
        pDeathEffectMaterial.SetFloat(_intensity, powerEndAmt);
        pDeathEffectMaterial.SetFloat(_power, intensityEndAmt);
        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float lerpedIntensity = Mathf.Lerp(intensityEndAmt, 0f, (elapsedTime / _duration));
            float lerpedPower = Mathf.Lerp(powerEndAmt, 0f, (elapsedTime / _duration));
            pDeathEffectMaterial.SetFloat(_power, lerpedPower);
            pDeathEffectMaterial.SetFloat(_intensity, lerpedIntensity);
            yield return null;
        }

    }
}
