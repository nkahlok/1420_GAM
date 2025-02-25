using UnityEngine;

public class QToContinueScript : MonoBehaviour
{
    public LevelLoaderScript levelLoader;
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            levelLoader.LoadNextLevel();
        }
    }
}
