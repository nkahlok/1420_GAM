using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startingPos; //starting position of sprites
    private float lengthOfSprite;    //length of sprites
    public float parallaxAmtX;  //amount of parallax scroll on x
    public PlayerCam MainCamera;   //reference of the camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Getting the starting X position of sprite
        startingPos = transform.position.x;
        //length of sprites
        lengthOfSprite = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movementX = MainCamera.transform.position.x * (1 - parallaxAmtX);
        float distanceX = MainCamera.transform.position.x * parallaxAmtX; // 0 = moves with cam 1 = doesn't move with cam

        Vector3 NewPosition = new Vector3(startingPos + distanceX, transform.position.y, transform.position.z);

        //if background has reached end of length adjust its position
        if (movementX > startingPos + lengthOfSprite)
        {
            startingPos += lengthOfSprite;
        }
        else if (movementX < startingPos - lengthOfSprite)
        {
            startingPos -= lengthOfSprite;
        }

        transform.position = NewPosition;
    }
}
