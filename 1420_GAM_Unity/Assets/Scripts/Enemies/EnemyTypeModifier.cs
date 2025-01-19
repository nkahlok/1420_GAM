using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeModifier : MonoBehaviour
{

    public int rankModifier;
    private int hits;
    public GameObject E;
    public GameObject D;
    public GameObject C;
    public GameObject B;
    public GameObject A;

    // Start is called before the first frame update
    void Start()
    {
        
        switch (rankModifier)
        {
            case 2:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 1.5f, this.transform.localScale.y * 1.5f);
                hits = 4;
                D.SetActive(true);
                break;
            case 3:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 2f, this.transform.localScale.y * 2f);
                hits = 6;
                C.SetActive(true);
                break;
            case 4:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 2.5f, this.transform.localScale.y * 2.5f);
                hits = 8;
                B.SetActive(true);
                break;
            case 5:
                //this.transform.localScale = new Vector2(this.transform.localScale.x * 3f, this.transform.localScale.y * 3f);
                hits = 10;
                A.SetActive(true);
                break;
            default:
                //this.transform.localScale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
                hits = 3;
                E.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
      if(hits == 0)
        {
            Destroy(this.gameObject); 
        }
    }

    public void Damage(int hitsMultiplier)
    {
        hits -= 1*hitsMultiplier;
    }
}
