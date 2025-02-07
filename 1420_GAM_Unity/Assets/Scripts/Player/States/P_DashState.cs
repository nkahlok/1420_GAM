using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DashState : PlayerState
{
    protected DashSkill skill2;

    public P_DashState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        skill2 = SkillManager.instance.dashSkill;
        skill2.DashDurCount = skill2.DashDur;
        player.dashEffect.Play();
        base.Enter();
    }

    public override void Exit()
    {
        player.SetVelocity(0, rb.linearVelocityY);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        

        player.SetVelocity(player.facingDir * skill2.dashSpeed, 0);

        if (skill2.DashDurCount < 0)
        {
              
            player.stateMachine.Changestate(player.fall);
        }

        if(player.isWall)
        {
            
            player.stateMachine.Changestate(player.fall);
        }

    }
}
