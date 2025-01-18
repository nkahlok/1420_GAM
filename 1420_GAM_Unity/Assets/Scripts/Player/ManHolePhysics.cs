using System.Xml.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ManHolePhysics : MonoBehaviour
{
    private Rigidbody2D rb;
    Player player;
    ManHoleSkill skill1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = PlayerManager.instance.player;
        skill1 = SkillManager.instance.manholeSkill;
        
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


    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(skill1.manholeSpeed * player.manHoleAim.xThrow, skill1.manholeSpeed * player.manHoleAim.yThrow);

        Destroy(gameObject, SkillManager.instance.manholeSkill.manHoleDur);
    }
}
