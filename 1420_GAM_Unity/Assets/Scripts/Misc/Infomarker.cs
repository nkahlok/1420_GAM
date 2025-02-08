using UnityEngine;

public class Infomarker : MonoBehaviour
{
    public GameObject tutorial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null) 
        {
            tutorial.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            tutorial.SetActive(false);
        }

    }

}
