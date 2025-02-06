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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Color color = tilemap.color;
            color.a = 0;
            Debug.Log("collided");
        }
        //if there's a gameobject with the tag "Player" then the train exterior's alpha value goes to 0
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Color color = tilemap.color;
            color.a = 255;
            Debug.Log("not collided");
        }
        //opposite happens when player exits
    }
}
