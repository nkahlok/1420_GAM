using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RavenousVfxManager : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private ScriptableRendererFeature _fullScreenVFX;
    [SerializeField] private Material _ravenousEffectMaterial;
    [SerializeField] private float intensityStartAmt = 2.25f;
    [SerializeField] private float powerStartAmt = 1.25f;
    [SerializeField] public static bool disabled = false;
    //[SerializeField] private ParticleSystem feathers, feathers2;

    private int _intensity = Shader.PropertyToID("_VignetteIntensity");
    private int _power = Shader.PropertyToID("_VignettePower");
    private int _noiseSpeed = Shader.PropertyToID("_NoiseSpeed");

    private void Start()
    {
        _fullScreenVFX.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {

            PlayRavenousEffect();

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("press k");
            DisableRavenousEffect();
        }
    }

    public void PlayRavenousEffect()
    {
        //feathers.Play();
        //feathers2.Play();
        StartCoroutine(PlayFullscreenEffect()); //play effect
    }

    public void DisableRavenousEffect()
    {
        StartCoroutine(DisableFullscreenEffect()); //disable effect
        //feathers2.Stop();
        //feathers.Stop();
    }

    private IEnumerator PlayFullscreenEffect()
    {
        _fullScreenVFX.SetActive(true);
        _ravenousEffectMaterial.SetFloat(_intensity, 0f);
        _ravenousEffectMaterial.SetFloat(_power, 0f);
        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            Debug.Log("im inside the while loop");
            elapsedTime += Time.deltaTime;
            float lerpedIntensity = Mathf.Lerp(0f, intensityStartAmt, (elapsedTime / _duration));
            float lerpedPower = Mathf.Lerp(0f, powerStartAmt, (elapsedTime / _duration));
            _ravenousEffectMaterial.SetFloat(_power, lerpedPower);
            _ravenousEffectMaterial.SetFloat(_intensity, lerpedIntensity);
            yield return null;
        }

        //while (elapsedTime < _fadeOutTime)
        //{
        //    elapsedTime += Time.deltaTime;
        //    float lerpedIntensity = Mathf.Lerp(intensityStartAmt, 0f, (elapsedTime / _fadeOutTime));
        //    float lerpedPower = Mathf.Lerp(powerStartAmt, 0f, (elapsedTime / _fadeOutTime));

        //    _material.SetFloat(_power, lerpedPower);
        //    _material.SetFloat(_intensity, lerpedIntensity);
        //    yield return null;
        //}
    }

    private IEnumerator DisableFullscreenEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float lerpedIntensity = Mathf.Lerp(intensityStartAmt, 0f, (elapsedTime / _duration));
            float lerpedPower = Mathf.Lerp(powerStartAmt, 0f, (elapsedTime / _duration));
            _ravenousEffectMaterial.SetFloat(_power, lerpedPower);
            _ravenousEffectMaterial.SetFloat(_intensity, lerpedIntensity);
            yield return null;
        }
        _fullScreenVFX.SetActive(false);
    }
}
