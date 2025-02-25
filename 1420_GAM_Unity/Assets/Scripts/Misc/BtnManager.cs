using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName: "Alpha");
    }

    public void MainMenuScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName: "Menu");
    }

}
