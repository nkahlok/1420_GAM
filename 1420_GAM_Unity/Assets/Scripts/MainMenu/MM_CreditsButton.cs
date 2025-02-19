using UnityEngine;

public class MM_CreditsButton : MonoBehaviour
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
        anim.Play("MM_HCreditsButton");
        Debug.Log("I am being hovered over");
    }
    private void OnMouseExit()
    {
        anim.Play("MM_ICreditsButton");
        Debug.Log("I am not being hovered over");
    }
    private void OnMouseDown()
    {

    }
}
