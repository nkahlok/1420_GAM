using UnityEngine;
using UnityEngine.SceneManagement;

public class QToContinueScript : MonoBehaviour
{
    public LevelLoaderScript levelLoader;
    public bool isCreditsScene = false;
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && !isCreditsScene)
        {
            levelLoader.LoadNextLevel();
        }
        if (Input.GetKey(KeyCode.Q) && isCreditsScene)
        {
            levelLoader.LoadMenu();
        }
    }
}
