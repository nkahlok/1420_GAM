//using System.Drawing;
//using UnityEditor.ShaderGraph;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class TrainExtScript : MonoBehaviour
{
    public TilemapCollider2D hitbox;
    public Tilemap tilemap;
    public Light2D characterLight;
    float startingAlpha;
    float endAlpha;
    float duration;
    float elapsdedTime;

    void Start()
    {
        hitbox = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<Tilemap>();
        duration = 0.5f;
        startingAlpha = 1f;
        endAlpha = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /*if(PlayerManager.instance.player.transform.position.y > this.gameObject.transform.position.y)
            {
                return;
            }
            else
            {*/
            //}
            //tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0f);

            elapsdedTime = 0;

            StartCoroutine("alphaFadeOut");

            characterLight.color = new Color(characterLight.color.r, characterLight.color.g, characterLight.color.b, 1f);
    
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /*if (PlayerManager.instance.player.transform.position.y > this.gameObject.transform.position.y)
            {
                return;
            }
            else
            {*/

            //}

            //StartCoroutine("alphaFadeIn");

            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0f);

            characterLight.color = new Color(characterLight.color.r, characterLight.color.g, characterLight.color.b, 1f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /*if (PlayerManager.instance.player.transform.position.y > this.gameObject.transform.position.y)
            {
                return;
            }
            else
            {*/
            // }

            //tilemap.color = tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 1f);


            elapsdedTime = 0;

            StartCoroutine("alphaFadeIn");

            characterLight.color = new Color(characterLight.color.r, characterLight.color.g, characterLight.color.b, 0f);
        }

    }

    IEnumerator alphaFadeOut()
    {
        while(elapsdedTime < duration)
        {
            elapsdedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startingAlpha, endAlpha, elapsdedTime/duration);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;

        }
    }

    IEnumerator alphaFadeIn()
    {
        while (elapsdedTime < duration)
        {
            elapsdedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(endAlpha, startingAlpha, elapsdedTime / duration);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;

        }
    }

}
