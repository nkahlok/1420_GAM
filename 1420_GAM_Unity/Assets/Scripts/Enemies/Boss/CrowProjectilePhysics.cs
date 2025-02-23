using UnityEngine;

public class CrowProjectilePhysics : MonoBehaviour
{

    private Player player;
    public float rotationSpeed;
    public float projectileSpeed;

    Quaternion rotateToTarget;
    Vector3 dir;

    Rigidbody2D rb;

    float loadingProjTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerManager.instance.player;
        rb = GetComponent<Rigidbody2D>();
        loadingProjTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.transform.position, this.transform.position) > 2)
        {
            dir = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rotateToTarget = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateToTarget, rotationSpeed * Time.deltaTime);

        }

        if (loadingProjTime < 0f)
            rb.linearVelocity = new Vector2(dir.x * projectileSpeed, dir.y * projectileSpeed);
        else
            rb.linearVelocity = new Vector2(0, 0);

        loadingProjTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

}
