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
        player.SetVelocity(0, player.jumpForce);
        player.coyoteEnabled = false;
        player.doubleJumpEnabled = !player.doubleJumpEnabled;
        SoundManager.PlaySfx(SoundType.PLAYERJUMP);
        SoundManager.PlaySfx(SoundType.PLAYERFEATHERS);
        player.jumpFeathers.Play();
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
            player.SetVelocity(0, rb.linearVelocity.y * 0.5f);
        }

        if (xInput != 0)
        {
            player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocity.y);
        }
  

        if (Input.GetButtonDown("Jump") && player.doubleJumpEnabled == true)
        {
            stateMachine.Changestate(player.jump);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && player.caneWpn)
        {
            stateMachine.Changestate(player.aerialAttack);
        }

    }
}
