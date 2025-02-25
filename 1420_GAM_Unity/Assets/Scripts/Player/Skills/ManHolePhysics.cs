using System.Collections;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class ManHolePhysics : MonoBehaviour
{
    [HideInInspector]public Rigidbody2D rb;
    Player player;
    ManHoleSkill skill1;
    [HideInInspector] public bool canHitEnemy;
    private float newSpeed;
    private int bounceCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canHitEnemy = true;

        //skill1.manHoleCount = skill1.manHoleDur;

        rb = GetComponent<Rigidbody2D>();

        player = PlayerManager.instance.player;
        skill1 = SkillManager.instance.manholeSkill;

        newSpeed = skill1.manholeSpeed;
        
        //skill1.newPrefab.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        if (player.manHoleAim.xThrow == 1 && player.manHoleAim.yThrow == 1|| player.manHoleAim.xThrow == -1 && player.manHoleAim.yThrow == 1 )
        {
            gameObject.transform.Rotate(0, 0, 45);
        }
        else if (player.manHoleAim.xThrow == 0 && player.manHoleAim.yThrow == 1)
        {
            gameObject.transform.Rotate(0, 0, 90);
        }
        else if (player.manHoleAim.xThrow == -1 && player.manHoleAim.yThrow == 1 || player.manHoleAim.xThrow == -1 && player.manHoleAim.yThrow == -1 || player.manHoleAim.xThrow == 1 && player.manHoleAim.yThrow == -1)
        {
            gameObject.transform.Rotate(0, 0, -45);
        }
        else if (player.manHoleAim.xThrow == 0 && player.manHoleAim.yThrow == -1)
        {
            gameObject.transform.Rotate(0, 0, 90);
        }

        rb.linearVelocity = new Vector2(skill1.manholeSpeed * player.manHoleAim.xThrow, skill1.manholeSpeed * player.manHoleAim.yThrow);

    }

    // Update is called once per frame
    void Update()
    {
        // rb.linearVelocity = new Vector2(skill1.manholeSpeed * player.manHoleAim.xThrow, skill1.manholeSpeed * player.manHoleAim.yThrow);

        //if(skill1.manHoleCount < 0)
        //{

        StartCoroutine("SetTrue", skill1.manHoleDur - 0.1f);

        Destroy(gameObject, skill1.manHoleDur);

        if(bounceCount >= skill1.maxBounces)
        {
            player.manholeAvailable = true;
            Destroy(gameObject);
        }

        //}

        //skill1.manHoleCount -= Time.deltaTime;  

    }

    IEnumerator SetTrue(float second)
    {
        yield return new WaitForSeconds(second);
        player.manholeAvailable = true;
    }

    public void Bounce()
    {
        newSpeed *= 1.2f;
        rb.linearVelocity = new Vector2(newSpeed * player.manHoleAim.xThrow, newSpeed * player.manHoleAim.yThrow);
        skill1.manHoleCount = skill1.manHoleDur;
        if (skill1.knockBackForce.x < 0)
        {
            skill1.knockBackForce.x *= -1;
        }
        //Debug.Log("Bounced");

        canHitEnemy = !canHitEnemy;
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() && canHitEnemy)
        {
            //skill1.manHoleCount = skill1.manHoleDur;
            bounceCount++;  
            //Debug.Log("Manhole entered");
            newSpeed *= 1.2f;
            rb.linearVelocity = new Vector2(newSpeed * player.manHoleAim.xThrow * -1, newSpeed * player.manHoleAim.yThrow * -1);
            if (this.gameObject.transform.position.x > collision.gameObject.GetComponent<Enemy>().gameObject.transform.position.x)
            {
                skill1.knockBackForce.x *= -1;
            }
            collision.gameObject.GetComponent<Enemy>().KnockBack("Manhole");
            canHitEnemy = !canHitEnemy;
            
        }
        else if(collision.gameObject.GetComponent<Boss>() && canHitEnemy)
        {
            bounceCount++;
            newSpeed *= 1.2f;
            rb.linearVelocity = new Vector2(newSpeed * player.manHoleAim.xThrow * -1, newSpeed * player.manHoleAim.yThrow * -1);
            canHitEnemy = !canHitEnemy;

        }
        if (collision.gameObject.GetComponent<EnemyTypeModifier>())
        {
            player.comboHitCount = player.comboTime;
            collision.gameObject.GetComponent<EnemyTypeModifier>().Damage(1);
            
        }

        if (collision.gameObject.GetComponent<Player>() && !canHitEnemy)
        {
            player.manholeAvailable = true;
            Destroy(gameObject);
        }

        if(collision.gameObject.layer == 3)
        {
            player.manholeAvailable = true;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("ArenaBounds"))
        {
            player.manholeAvailable = true;
            Destroy(gameObject);
        }

    }

}
