using UnityEngine;

public class BirdWallPhysics : MonoBehaviour
{

    private Player player;
    private int dir;

    public int birdwallDmg;
    public float projectileSpeed;
    public Vector2 knockbackForce;


    Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerManager.instance.player;

        if (this.gameObject.transform.position.x > player.transform.position.x)
            dir = -1;
        else
            dir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(projectileSpeed*dir , 0);
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
                }
                else if (player.facingDir == -1)
                {
                    player.blockHitLeft.Play();
                }
                Destroy(this.gameObject);
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
                player.Damage(birdwallDmg);
                Destroy(this.gameObject);
            }


        }
        else if (collision.gameObject.CompareTag("ArenaBounds"))
        {
            Destroy(this.gameObject);
        }
        
    }

}
