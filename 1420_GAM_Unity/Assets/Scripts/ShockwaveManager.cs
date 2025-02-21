using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShockwaveManager : MonoBehaviour
{
    [SerializeField] private float _shockwavetime = 0.7f;
    private Coroutine _shockwaveCoroutine;
    private Material _material;
    private static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistFromCenter");

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("shockwave");
            CallShockwave();
        }
    }

    public void CallShockwave() //calls shockwave
    {
        _shockwaveCoroutine = StartCoroutine(ShockwaveAction(-0.1f, 1f));

    }

    private IEnumerator ShockwaveAction(float startPos, float endPos)
    {
        _material.SetFloat(_waveDistanceFromCenter, startPos);
        float lerpedAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < _shockwavetime)
        {
            elapsedTime += Time.deltaTime;
            lerpedAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / _shockwavetime));
            _material.SetFloat(_waveDistanceFromCenter, lerpedAmount);
            yield return null;
        }
    }
}
