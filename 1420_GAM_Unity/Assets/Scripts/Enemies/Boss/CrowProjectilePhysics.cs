using UnityEngine;

public class CrowProjectilePhysics : MonoBehaviour
{

    private Player player;
    public float rotationSpeed;
    public float projectileSpeed;

    Quaternion rotateToTarget;
    Vector3 dir;

    Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerManager.instance.player;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        dir = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotateToTarget = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp (transform.rotation, rotateToTarget, rotationSpeed * Time.deltaTime);
            
        rb.linearVelocity = new Vector2(dir.x*projectileSpeed, dir.y*projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

}
