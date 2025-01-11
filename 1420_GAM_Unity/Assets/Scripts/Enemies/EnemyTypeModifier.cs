using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeModifier : MonoBehaviour
{

    private int randomNum => Random.Range(1, 5);
    private int health;
    public GameObject E;
    public GameObject D;
    public GameObject C;
    public GameObject B;
    public GameObject A;

    // Start is called before the first frame update
    void Start()
    {
        
        switch (randomNum)
        {
            case 2:
                this.transform.localScale = new Vector2(this.transform.localScale.x * 1.5f, this.transform.localScale.y * 1.5f);
                D.SetActive(true);
                break;
            case 3:
                this.transform.localScale = new Vector2(this.transform.localScale.x * 2f, this.transform.localScale.y * 2f);
                C.SetActive(true);
                break;
            case 4:
                this.transform.localScale = new Vector2(this.transform.localScale.x * 2.5f, this.transform.localScale.y * 2.5f);
                B.SetActive(true);
                break;
            case 5:
                this.transform.localScale = new Vector2(this.transform.localScale.x * 3f, this.transform.localScale.y * 3f);
                A.SetActive(true);
                break;
            default:
                this.transform.localScale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
                E.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(randomNum);
    }
}
