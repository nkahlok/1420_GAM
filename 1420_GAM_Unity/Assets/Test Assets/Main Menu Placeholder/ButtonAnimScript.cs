using UnityEngine;

public class ButtonAnimScript : MonoBehaviour
{
    private Animation anim;
    public BoxCollider startButton;
    private void Start()
    {
        startButton = GetComponent<BoxCollider>();
        anim = GetComponent<Animation>();
        Debug.Log("Start");
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

    private void OnMouseDown()
    {
        Debug.Log("I am being pressed");
    }
}
