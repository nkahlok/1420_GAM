using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTypeModifier : MonoBehaviour
{
    [HideInInspector] public bool canBeDamaged;
    private Enemy enemy;
    [Range(1,6)]public int rankModifier;
    public int bossHP;
     public int hits;
    private int storedHits;
    private SpriteRenderer spriteRenderer;
    private Color color;
    //public Text rankName;
    private Player player;
    public GameObject D;
    public GameObject C;
    public GameObject B;
    public GameObject A;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        color = spriteRenderer.color;

        player = PlayerManager.instance.player;
        enemy = GetComponent<Enemy>();  
        
        switch (rankModifier)
        {
            case 1:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 1.5f, this.transform.localScale.y * 1.5f);
                hits = player.comboNum[0];
                //rankName.text = player.comboNames[0];
                D.SetActive(true);
                break;
            case 2:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 2f, this.transform.localScale.y * 2f);
                hits = player.comboNum[1];
                //rankName.text = player.comboNames[1];
                C.SetActive(true);
                break;
            case 3:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 2.5f, this.transform.localScale.y * 2.5f);
                hits = player.comboNum[2];
                //rankName.text = player.comboNames[2];
                B.SetActive(true);
                break;
            case 4:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 3f, this.transform.localScale.y * 3f);
                hits = player.comboNum[3];
                //rankName.text = player.comboNames[3];
                A.SetActive(true);
                break;
            case 5:
                hits = bossHP;
                break;
                default:
                hits = 1000;
                break;

        }


        storedHits = hits;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.instance.player.comboHits == 0 && rankModifier != 5)
        {
            hits = storedHits;
        }

        //Debug.Log($"hits:{hits}");

        if(hits == 0 && Time.timeScale != 0)
        {
            //PlayerManager.instance.player.comboHits = 0;
            enemy.enemyStateMachine.Changestate(enemy.enemyDeathState);
            canBeDamaged = false;
        }

        A.gameObject.transform.rotation = Quaternion.identity;
        B.gameObject.transform.rotation = Quaternion.identity;
        C.gameObject.transform.rotation = Quaternion.identity;
        D.gameObject.transform.rotation = Quaternion.identity;

        /* if(hits == player.comboNum[0])
         {
             rankName.text = player.comboNames[0];
         }
         else if(hits == player.comboNum[1])
         {
             rankName.text = player.comboNames[1];
         }
         else if (hits == player.comboNum[2])
         {
             rankName.text = player.comboNames[2];
         }
         else if((hits == player.comboNum[3]))
         {
             rankName.text = player.comboNames[3];
         }*/

    }

    public void Damage(int hitsMultiplier)
    {
        if(!canBeDamaged)
            return;

        Debug.Log("Damaged");
        PlayerManager.instance.player.comboHits++;
        hits = hits - hitsMultiplier;
        StartCoroutine("SpriteHit", 0.2f);
    }

    IEnumerator SpriteHit(float seconds)
    {

        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = color;
    }

}
