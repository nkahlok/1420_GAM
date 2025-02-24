using UnityEngine;

public class ShockwavePhysics : MonoBehaviour
{

    public bool isRight;
    public float shockwaveSpeed;
    public int shockwaveDamage;
    public Vector2 knockbackForce;
    private Rigidbody2D rb;
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerManager.instance.player;

        if (isRight)
        {
            this.gameObject.transform.Rotate(0, 180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isRight)
        {
            rb.linearVelocity = new Vector2(shockwaveSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(shockwaveSpeed * -1, 0);
        }

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
                player.Damage(shockwaveDamage);

                Destroy(this.gameObject);
            }


        }
        else if (collision.gameObject.CompareTag("ArenaBounds"))
        {
            Destroy(this.gameObject);
        }

    }
}
