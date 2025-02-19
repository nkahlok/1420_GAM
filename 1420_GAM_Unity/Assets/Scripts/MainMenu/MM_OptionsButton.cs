using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_OptionsButton : MonoBehaviour
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

    }
}
