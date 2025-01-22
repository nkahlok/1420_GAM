using UnityEngine;

public class StartButton : MonoBehaviour
{
    public Animation anim;
    public BoxCollider box;
    private void Start()
    {
        box = GetComponent<BoxCollider>();
        anim = GetComponent<Animation>();
    }
    private void OnMouseEnter()
    {
        anim.Play("HStart");
        Debug.Log("I am being hovered over");
    }
    private void OnMouseExit()
    {
        anim.Play("MainMenuIdleAnim");
        Debug.Log("I am not being hovered over");
    }
}
