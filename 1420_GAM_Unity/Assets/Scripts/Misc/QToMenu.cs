using UnityEngine;
using UnityEngine.SceneManagement;

public class QToMenu : MonoBehaviour
{
    public LevelLoaderScript levelLoader;
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            levelLoader.LoadMenu();
        }
    }
}
