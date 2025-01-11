using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : Skill
{
    // Start is called before the first frame update

    public float DashDur;
    public float dashSpeed;
    [HideInInspector] public float DashDurCount;
    public override void CastSkill()
    {
        base.CastSkill();
        player.stateMachine.Changestate(player.dash);
    }

    protected override void Update()
    {
        base.Update();
        DashDurCount -= Time.deltaTime;
       
    }
}
