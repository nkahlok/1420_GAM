using UnityEngine;

public class TrainTransitionScript : MonoBehaviour
{
    public BoxCollider2D bc;
    public LevelLoaderScript levelLoader;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelLoader.LoadNextLevel();
        }
    }

}
