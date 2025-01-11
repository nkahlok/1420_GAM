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
    }

    public override void Update()
    {
        base.Update();

        stateDur -= Time.deltaTime;

        player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, player.attackMovement[comboCounter].y);

        if (stateDur < 0)
        {
            player.SetVelocity(0, 0);
        }


    }
}
