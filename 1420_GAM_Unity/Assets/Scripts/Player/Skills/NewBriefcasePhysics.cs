using UnityEngine;

public class NewBriefcasePhysics : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    Player player;
    ManHoleSkill skill1;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = PlayerManager.instance.player;
        skill1 = SkillManager.instance.manholeSkill;

        rb.linearVelocity = new Vector2(skill1.manholeSpeed * player.facingDir, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {

            collision.gameObject.GetComponent<Enemy>().KnockBack("Manhole");
            Destroy(this.gameObject);

        }

        if (collision.gameObject.GetComponent<EnemyTypeModifier>())
        {
            player.comboHitCount = player.comboTime;
            collision.gameObject.GetComponent<EnemyTypeModifier>().Damage(1);

        }


        /*if (collision.gameObject.layer == 3)
        {
            player.manholeAvailable = true;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("ArenaBounds"))
        {
            player.manholeAvailable = true;
            Destroy(gameObject);
        }*/

    }

}
