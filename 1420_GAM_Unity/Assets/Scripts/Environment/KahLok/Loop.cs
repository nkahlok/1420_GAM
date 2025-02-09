using UnityEngine;

public class Loop : MonoBehaviour
{
    public float movSpeed;
    Vector3 startPos;
    float repeatWidth;
    //public Camera cam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x/3;
        //repeatWidth = 2f * cam.orthographicSize * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * movSpeed);
        if(transform.position.x < startPos.x - repeatWidth)
        {
            transform .position = startPos;
        }

    }
}
