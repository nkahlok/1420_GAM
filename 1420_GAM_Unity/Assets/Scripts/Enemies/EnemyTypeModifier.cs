using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTypeModifier : MonoBehaviour
{

    [Range(1,4)]public int rankModifier;
    private int hits;
    public Text rankName;
    /*public GameObject D;
    public GameObject C;
    public GameObject B;
    public GameObject A;*/

    // Start is called before the first frame update
    void Start()
    {
        Player player = PlayerManager.instance.player;
        
        switch (rankModifier)
        {
            case 1:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 1.5f, this.transform.localScale.y * 1.5f);
                hits = 8;
                rankName.text = player.comboNames[0];
                //D.SetActive(true);
                break;
            case 23:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 2f, this.transform.localScale.y * 2f);
                hits = 12;
                rankName.text = player.comboNames[1];
                //C.SetActive(true);
                break;
            case 3:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 2.5f, this.transform.localScale.y * 2.5f);
                hits = 16;
                rankName.text = player.comboNames[2];
                //B.SetActive(true);
                break;
            case 4:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 3f, this.transform.localScale.y * 3f);
                hits = 20;
                rankName.text = player.comboNames[3];
                //A.SetActive(true);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log($"hits:{hits}");

        if(hits == 0)
        {
            Destroy(this.gameObject); 
        }

        rankName.gameObject.transform.rotation = Quaternion.identity;   

    }

    public void Damage(int hitsMultiplier)
    {
        PlayerManager.instance.player.comboHits++;
        hits = hits - hitsMultiplier;
    }
}
