using UnityEngine;

public class StartButton : MonoBehaviour
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
        anim.Play("HStart");
        Debug.Log("I am being hovered over");
    }
    private void OnMouseExit()
    {
        anim.Play("IStart");
        Debug.Log("I am not being hovered over");
    }

    
}
