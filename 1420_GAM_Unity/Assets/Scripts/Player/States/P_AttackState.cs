using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AttackState : PlayerState

{
    protected int comboCounter;

    public P_AttackState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if(comboCounter > 2 || player.comboCount < 0)
        {
            comboCounter = 0;
        }

        //player.SetVelocity(0,0);    

        anim.SetInteger("ComboCounter1", comboCounter);
        
        player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, player.attackMovement[comboCounter].y);

        player.comboCount = player.comboTime;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, player.attackMovement[comboCounter].y);
    }
}
