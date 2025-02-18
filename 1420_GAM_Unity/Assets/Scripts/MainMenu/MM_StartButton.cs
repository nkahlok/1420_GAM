using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_StartButton : MonoBehaviour
{
    public Animator anim;
    public BoxCollider box;
    private void Start()
    {
        box = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        anim.Play("MM_HStartButton");
        Debug.Log("I am being hovered over");
    }
    private void OnMouseExit()
    {
        anim.Play("MM_IStartButton");
        Debug.Log("I am not being hovered over");
    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName: "Alpha");
    }
}
