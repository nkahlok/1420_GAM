using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_FallState : PlayerState
{
    public P_FallState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
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

        if (player.isGround && rb.linearVelocity.y == 0)
        {
            stateMachine.Changestate(player.idle);
        }

        if (xInput != 0)
        {
            player.SetVelocity(xInput * player.moveSpeed * 0.7f, rb.linearVelocity.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
           
            player.jumpBufferCount = player.jumpBufferTime;
            Debug.Log("Space bar pressed");
        }

       if (player.coyoteEnabled == true && player.coyoteCount > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                stateMachine.Changestate(player.jump);
            }
        }
    }
}