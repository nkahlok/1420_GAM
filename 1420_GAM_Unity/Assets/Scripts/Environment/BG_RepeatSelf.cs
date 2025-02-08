using Unity.VisualScripting;
using UnityEngine;

public class BG_RepeatSelf : MonoBehaviour
{
    private float startingPos; //starting position of sprites
    private float lengthOfSprite;    //length of sprites
    public PlayerCam MainCamera;   //reference of the camera
    void Start()
    {
        //Getting the starting X position of sprite
        startingPos = transform.position.x;
        //length of sprites
        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float movementX = transform.position.x;
        if (movementX > startingPos + lengthOfSprite)
        {
            startingPos += lengthOfSprite;
        }
        else if (movementX < startingPos - lengthOfSprite)
        {
            startingPos -= lengthOfSprite;

        }
    }
}
