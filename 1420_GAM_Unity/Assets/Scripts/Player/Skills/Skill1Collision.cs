using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1Collision : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = PlayerManager.instance.player;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 6)
        {
            
            Destroy(this.gameObject);
        }
    }
}
