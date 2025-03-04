using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public Animator folderAnimator;
    public Animator optionsAnimator;
    public 

    void Start()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Resume()
    {
        StartCoroutine(Resuming());
    }
    private IEnumerator Resuming()
    {
        folderAnimator.Play("FolderExitAnim");
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(0.5f);
        pauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        folderAnimator.Play("FolderIntroAnim");
    }

    public void OpenOptions()
    {
        optionsMenuUI.SetActive(true);
        optionsAnimator.Play("VolOptIntro");
    }
    public void CloseOptions()
    {
        StartCoroutine(ClosingOpt());
    }

    private IEnumerator ClosingOpt()
    {
        optionsAnimator.Play("VolOptExit");
        yield return new WaitForSecondsRealtime(5);
        optionsMenuUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
