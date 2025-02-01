using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 || collision.gameObject.layer == 3)
        {
            Destroy(this.gameObject);
        }
    }

}
