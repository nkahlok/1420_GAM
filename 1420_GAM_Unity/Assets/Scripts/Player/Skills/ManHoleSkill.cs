using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ManHoleSkill : Skill
{

   
    public GameObject prefab;
    [HideInInspector] public GameObject newPrefab;
    public float manHoleDur;
    [HideInInspector] public float manHoleCount;
    public float manholeSpeed;
    public Vector2 knockBackForce;
    public int maxBounces;


    public override void CastSkill()
    {
        base.CastSkill();

        //Debug.Log("Skill 1 is used");

        PlayerManager.instance.player.stateMachine.Changestate(PlayerManager.instance.player.manHoleAim);
        
       

        //newPrefab.transform.position = new Vector2(player.transform.position.x + player.facingDir * 2,player.transform.position.y);

        //newPrefab.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(bulletSpeed * player.facingDir, 0);
    }

    public void ThrowManHole()
    {

        GameObject[] currentmanHole = GameObject.FindGameObjectsWithTag("ManHole");

        if(knockBackForce.x < 0)
        {
          knockBackForce.x *= -1;
        }

        if(currentmanHole != null )
        {
            foreach(GameObject manhole in currentmanHole)
            {
                Destroy(manhole);
            }
        }

        newPrefab = prefab;
        Instantiate(newPrefab, player.manholeThrowChecker.position, player.manholeThrowChecker.rotation);
        

        
        //newPrefab.transform.position = new Vector2(player.transform.position.x + player.facingDir*2f, player.transform.position.y);
        //newPrefab.transform.position = new Vector2(player.manholeThrowChecker.position.x, player.manholeThrowChecker.position.y);
        //newPrefab.transform.position = player.manholeThrowChecker.transform.position;
    }

    protected override void Update()
    {
        base.Update();  

        


      

    }


}
