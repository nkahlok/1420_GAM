using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    private Player player;
    public Vector2 knockbackForce;
    public int bulletDmg;

    void Start()
    {
        player = PlayerManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            if ((player.isShield && player.facingDir == 1 && this.gameObject.transform.position.x > player.transform.position.x) || (player.isShield && player.facingDir == -1 && this.gameObject.transform.position.x < player.transform.position.x))
            {
                if(player.facingDir == 1)
                {
                    player.blockHitRight.Play();
                    SoundManager.PlaySfx(SoundType.PLAYERBLOCKSUCCESS);
                }
                else if(player.facingDir == -1)
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
                player.Damage(bulletDmg/2);    
            }
            
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            Destroy(this.gameObject);
        }
    }

}
