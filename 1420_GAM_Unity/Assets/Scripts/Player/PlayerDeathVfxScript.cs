using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerDeathVfxScript : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private ScriptableRendererFeature _fullScreenVFX;
    [SerializeField] private Material pDeathEffectMaterial;
    [SerializeField] private float powerEndAmt = 10f;
    [SerializeField] private float intensityEndAmt = 0f;
    private int _intensity = Shader.PropertyToID("_VignetteIntensity");
    private int _power = Shader.PropertyToID("_VignettePower");
    private void Start()
    {
        _fullScreenVFX.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Dead");
            PlayDeathEffect();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("NotDead");
            ReverseDeathEffect();
        }
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
        _fullScreenVFX.SetActive(true);
        pDeathEffectMaterial.SetFloat(_intensity, 0f);
        pDeathEffectMaterial.SetFloat(_power, 0f);
        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float lerpedIntensity = Mathf.Lerp(0f, intensityEndAmt, (elapsedTime / _duration));
            float lerpedPower = Mathf.Lerp(0f, powerEndAmt, (elapsedTime / _duration));
            pDeathEffectMaterial.SetFloat(_power, lerpedPower);
            pDeathEffectMaterial.SetFloat(_intensity, lerpedIntensity);
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
        _fullScreenVFX.SetActive(true);
    }
}
