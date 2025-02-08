using Unity.VisualScripting;
using UnityEngine;

public class BG_RepeatSelf : MonoBehaviour
{
    Vector3 startingPos; //starting position of sprites
    Vector3 lengthOfSprite;    //length of sprites
    void Start()
    {
        //Getting the starting X position of sprite
        startingPos = transform.position;
        //length of sprites
        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size;
    }

    void Update()
    {
        float movementX = transform.position.x;
        if (movementX > startingPos.x + lengthOfSprite.x)
        {
            transform.position += lengthOfSprite;
        }
        else if (movementX < startingPos.x - lengthOfSprite.x)
        {
            startingPos -= lengthOfSprite;

        }
    }
}
