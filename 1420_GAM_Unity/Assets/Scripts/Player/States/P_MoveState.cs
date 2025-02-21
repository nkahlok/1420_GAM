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
        player.dustEffect.Play();
        moving = true;
 
    }

    public override void Exit()
    {
        base.Exit();
        player.dustEffect.Stop();
        Debug.Log("Exiting move");
        moving = false;


    }

    public override void Update()
    {
        base.Update();

        //Debug.Log("Im moving");


        if (player.cannotBeKnocked)
            player.rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocity.y);
        else
            player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.y < 0)
        {
            player.coyoteEnabled = true;
            player.coyoteCount = player.coyoteTime;
        }

    }
}
