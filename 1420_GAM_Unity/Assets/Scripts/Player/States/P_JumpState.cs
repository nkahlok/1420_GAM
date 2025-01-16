using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_JumpState : PlayerState
{
    public P_JumpState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
        player.coyoteEnabled = false;
        player.doubleJumpEnabled = !player.doubleJumpEnabled;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
      
        if (rb.linearVelocity.y < 0)
        {
            stateMachine.Changestate(player.fall);
        }

        if (Input.GetButtonUp("Jump"))
        {
            player.SetVelocity(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (xInput != 0)
        {
            player.SetVelocity(xInput * player.moveSpeed * 0.5f, rb.linearVelocity.y);
        }

        if (Input.GetButtonDown("Jump") && player.doubleJumpEnabled == true)
        {
            stateMachine.Changestate(player.jump);
        }
      
    }
}
