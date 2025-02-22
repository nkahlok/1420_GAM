using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RavenousVfxManager : MonoBehaviour
{
    [SerializeField] private float _displayTime = 1.5f;
    [SerializeField] private float _fadeOutTime = 0.5f;
    [SerializeField] private ScriptableRendererFeature _fullScreenVFX;
    [SerializeField] private Material _material;

    private int _intensity = Shader.PropertyToID("_VignetteIntensity");
    private int _power = Shader.PropertyToID("_VignettePower");
    private int _noiseSpeed = Shader.PropertyToID("_NoiseSpeed");

    private const float intensityStartAmt = 1.25f;
    private const float powerStartAmt = 1.25f;

    private void Start()
    {
        _fullScreenVFX.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(PlayFullscreenEffect());
        }
    }

    private IEnumerator PlayFullscreenEffect()
    {
        _fullScreenVFX.SetActive(true);
        _material.SetFloat(_intensity, intensityStartAmt);
        _material.SetFloat(_power, powerStartAmt);
        yield return new WaitForSeconds(_displayTime);
        float elapsedTime = 0f;
        while (elapsedTime < _fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedIntensity = Mathf.Lerp(intensityStartAmt, 0f, (elapsedTime / _fadeOutTime));
            float lerpedPower = Mathf.Lerp(powerStartAmt, 0f, (elapsedTime / _fadeOutTime));

            _material.SetFloat(_power, lerpedPower);
            _material.SetFloat(_intensity, lerpedIntensity);
            yield return null;
        }
        _fullScreenVFX.SetActive(false);
    }
}
