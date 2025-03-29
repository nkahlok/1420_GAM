using UnityEngine;

public class CrowProjectilePhysics : MonoBehaviour
{

    private Player player;
    public float rotationSpeed;
    public int bulletDmg;
    public float projectileSpeed;
    public Vector2 knockbackForce;

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
        if(Vector2.Distance(player.transform.position, this.transform.position) > 3)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if ((player.isShield && player.facingDir == 1 && this.gameObject.transform.position.x > player.transform.position.x) || (player.isShield && player.facingDir == -1 && this.gameObject.transform.position.x < player.transform.position.x))
            {
                if (player.facingDir == 1)
                {
                    player.blockHitRight.Play();
                    SoundManager.PlaySfx(SoundType.PLAYERBLOCKSUCCESS);
                }
                else if (player.facingDir == -1)
                {
                    player.blockHitLeft.Play();
                    SoundManager.PlaySfx(SoundType.PLAYERBLOCKSUCCESS);

                }
            }
            else
            {
                if (this.gameObject.transform.position.x > player.transform.position.x && player.facingDir == -1)
                {
                    player.KnockBack(knockbackForce.x * -1, knockbackForce.y);
                }
                else if (this.gameObject.transform.position.x < player.transform.position.x && player.facingDir == 1)
                {
                    player.KnockBack(knockbackForce.x * -1, knockbackForce.y);
                }
                else
                {
                    player.KnockBack(knockbackForce.x, knockbackForce.y);
                }

                Debug.Log("bullet hit");
                player.hitEffect.Play();
                player.Damage(bulletDmg);
            }

           
        }

        Destroy(this.gameObject);
    }

}
