using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    private Player player;
    public Vector2 knockbackForce;

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
                player.hitEffect.Play();
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
                
                player.Damage();    
            }


            Destroy(this.gameObject);
        }
        else if(collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            Destroy(this.gameObject);
        }
    }

}
