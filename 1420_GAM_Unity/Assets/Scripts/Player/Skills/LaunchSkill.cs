using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSkill : Skill
{
    public Vector2[] launchVelocity;
    public bool launchUp;
    public bool launchDown;
   

    public override void CastSkill()
    {
        base.CastSkill();

   
        player.stateMachine.Changestate(player.launchAttack);

      

    }

    protected override void Update()
    {
        base.Update();

        

        if (Input.GetAxisRaw("Vertical") == 1 && player.isGround)
        {

            StartCoroutine(launchDirRetain(1f, 1));
        }
        else if (Input.GetAxisRaw("Vertical") == -1 && !player.isGround)
        {

            StartCoroutine(launchDirRetain(1f, -1));
        }
     
     
    }

    private IEnumerator launchDirRetain(float waitTime, int dir)
    {
        if(dir == 1)
        {
            launchUp = true;
        }
        if(dir == -1)
        {
            launchDown = true;
        }
        yield return new WaitForSeconds(waitTime);
        launchDown = false;
        launchUp = false;
    }
}
