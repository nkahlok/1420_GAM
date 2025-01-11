using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MoveState : P_GroundedState
{
    public P_MoveState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
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

        player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.y < 0)
        {
            player.coyoteEnabled = true;
            player.coyoteCount = player.coyoteTime;
        }

    }
}
