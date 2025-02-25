//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    //deprecated, use MM_StartButton script
    public Animator anim;
    public BoxCollider box;
    private void Start()
    {
        box = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        anim.Play("HStart");
        Debug.Log("I am being hovered over");
    }
    private void OnMouseExit()
    {
        anim.Play("IStart");
        Debug.Log("I am not being hovered over");
    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName: "Alpha");
    }

}
