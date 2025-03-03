using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class StreetLamp : MonoBehaviour
{
    public GameObject lightSource;
    public Light2D spotlight;
    public SpriteRenderer lightImage;
    public float lightSpeed = 1.0f;
    public float targetIntensity = 3.05f;
    public float targetColor = 1.0f;

    private void Start()
    {
        spotlight = GetComponentInChildren<Light2D>();
        lightImage = transform.Find("lightOn")?.GetComponent<SpriteRenderer>();

        if (lightSource != null)
        {
            spotlight.intensity = 0f;
            lightImage.color = new Color(lightImage.color.r, lightImage.color.g, lightImage.color.b, 0f);
            lightSource.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            if (lightSource.activeSelf == false)
            {
                lightSource.SetActive(true); // Turn on the light
                StartCoroutine(SmoothTurnOnLight());
            }
        }
    }

    private IEnumerator SmoothTurnOnLight()
    {
        float currentIntensity = spotlight.intensity;
        float currentColor = lightImage.color.a;
        
        while (currentIntensity < targetIntensity)
        {
            currentIntensity += Time.deltaTime * lightSpeed;
            currentColor += Time.deltaTime * lightSpeed;
            float newAlpha = Mathf.Lerp(0f, 1f, targetColor);
            spotlight.intensity = Mathf.Clamp(currentIntensity, 0 , targetIntensity);
            lightImage.color = new Color(lightImage.color.r, lightImage.color.g, lightImage.color.b, newAlpha);
            yield return null;
        }
    }
}
