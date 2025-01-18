using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AttackState : PlayerState

{
    protected int comboCounter;
    protected float stateDur;


    public P_AttackState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateDur = 0.2f;
        
        if(comboCounter > 2 || player.comboCount < 0)
        {
            comboCounter = 0;
        }

        //player.SetVelocity(0,0);    

        anim.SetInteger("ComboCounter1", comboCounter);
        

        player.comboCount = player.comboTime;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        player.isBusy = false;
        rb.gravityScale = 3;
    }

    public override void Update()
    {
        base.Update();

        stateDur -= Time.deltaTime;

        if(player.isGround)
        {
            player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, player.attackMovement[comboCounter].y);
        }
        else
        {
            player.SetVelocity(0,0);
        }

        if (stateDur < 0)
        {
            player.SetVelocity(0, 0);
        }

        player.isBusy = true;

        rb.gravityScale = 0;

    }
}
