using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MM_OptionsButton : MonoBehaviour
{
    public Animator anim;
    public BoxCollider box;
    public GameObject optionsMenuUI;
    public GameObject pauseMenuUI;
    public Animator optionsAnimator;
    private void Start()
    {
        box = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    private void OnMouseEnter()
    {
        anim.Play("MM_HOptionsButton");
        Debug.Log("I am being hovered over");
    }
    private void OnMouseExit()
    {
        anim.Play("MM_IOptionsButton");
        Debug.Log("I am not being hovered over");
    }
    private void OnMouseDown()
    {
        OpenOptions();
    }
    public void OpenOptions()
    {
        optionsMenuUI.SetActive(true);
        Debug.Log("Click!");
        optionsAnimator.Play("VolOptIntro");
    }
    public void CloseOptions()
    {
        StartCoroutine(ClosingOpt());
    }
    private IEnumerator ClosingOpt()
    {
        optionsAnimator.Play("VolOptExit");
        yield return new WaitForSecondsRealtime(0.3f);
        optionsMenuUI.SetActive(false);
    }
}
