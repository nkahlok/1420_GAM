using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_StartButton : MonoBehaviour
{
    public Animator anim;
    public BoxCollider box;
    public LevelLoaderScript levelLoader;
    public GameObject optionsMenuUi;
    private void Start()
    {
        box = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        if (!optionsMenuUi.activeSelf)
        {
            anim.Play("MM_HStartButton");
            Debug.Log("I am being hovered over");
        }
    }
    private void OnMouseExit()
    {
        if (!optionsMenuUi.activeSelf)
        {
            anim.Play("MM_IStartButton");
            Debug.Log("I am not being hovered over");
        }
    }
    private void OnMouseDown()
    {
        if (!optionsMenuUi.activeSelf)
        {
            levelLoader.LoadNextLevel();
        }      
    }
}
