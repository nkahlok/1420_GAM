//using System.Drawing;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrainExtScript : MonoBehaviour
{
    public TilemapCollider2D hitbox;
    public Tilemap tilemap;
    void Start()
    {
        hitbox = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(PlayerManager.instance.player.transform.position.y > this.gameObject.transform.position.y)
            {
                return;
            }
            else
            {
                tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0f);  
            }
    
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerManager.instance.player.transform.position.y > this.gameObject.transform.position.y)
            {
                return;
            }
            else
            {

                tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0f);
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerManager.instance.player.transform.position.y > this.gameObject.transform.position.y)
            {
                return;
            }
            else
            {
                tilemap.color = tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 1f);
            }

        }

    }
}
