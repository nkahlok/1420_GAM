using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerDeathVfxScript : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private ScriptableRendererFeature _fullScreenVFX;
    [SerializeField] private Material playerdeathEffectMaterial;
    private int _intensity = Shader.PropertyToID("_VignetteIntensity");
    private int _power = Shader.PropertyToID("_VignettePower");
    private void Start()
    {
        _fullScreenVFX.SetActive(false);
    }

    private IEnumerator PlayFullscreenEffect()
    {
        _fullScreenVFX.SetActive(true);
        playerdeathEffectMaterial.SetFloat(_intensity, 0f);
        playerdeathEffectMaterial.SetFloat(_power, 0f);
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {

        }
        yield return null;
    }
}
