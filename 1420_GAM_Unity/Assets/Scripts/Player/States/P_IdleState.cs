using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_IdleState : P_GroundedState
{
    public P_IdleState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.cannotBeKnocked)
            player.rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocity.y);
        else
            player.SetVelocity(0, rb.linearVelocity.y);

    }
}
