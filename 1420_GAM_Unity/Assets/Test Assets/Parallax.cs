using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startingPos; //starting position of sprites
    private float lengthOfSprite;    //length of sprites
    public float AmountOfParallax;  //amount of parallax scroll
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
    void Update()
    {
        Vector3 Position = MainCamera.transform.position;
        float Temp = Position.x * (1 - AmountOfParallax);
        float Distance = Position.x * AmountOfParallax;

        Vector3 NewPosition = new Vector3(startingPos + Distance, transform.position.y, transform.position.z);

        transform.position = NewPosition;
    }
}
