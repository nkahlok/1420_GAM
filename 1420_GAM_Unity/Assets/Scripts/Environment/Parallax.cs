 using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startingPos; //starting position of sprites
    private float lengthOfSprite;    //length of sprites
    public float parallaxAmtX;  //amount of parallax scroll on x
    public PlayerCam MainCamera;   //reference of the camera

    void Start()
    {
        //Getting the starting X position of sprite
        startingPos = transform.position.x;
        //length of sprites
        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float movementX = MainCamera.transform.position.x * (1 - parallaxAmtX);
        float distanceX = MainCamera.transform.position.x * parallaxAmtX; // 1 = moves with cam 0 = doesn't move with cam

        transform.position = new Vector3(startingPos + distanceX, transform.position.y, transform.position.z);

        //if background has reached end of length adjust its position
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
