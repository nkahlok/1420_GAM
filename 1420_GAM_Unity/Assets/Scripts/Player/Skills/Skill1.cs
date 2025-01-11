using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Skill1 : Skill
{

   
    public GameObject prefab;
    private GameObject newPrefab;
    public float bulletSpeed;
    public float bulletDur;

  

    public override void CastSkill()
    {
        base.CastSkill();

        Debug.Log("Skill 1 is used");

        newPrefab = (GameObject)Instantiate(prefab);
        
        newPrefab.transform.position = new Vector2(player.transform.position.x + player.facingDir * 2,player.transform.position.y);

        newPrefab.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(bulletSpeed * player.facingDir, 0);
    }

    protected override void Update()
    {
        base.Update();  

        
        Destroy(newPrefab, bulletDur);

      

    }


}
