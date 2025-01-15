using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_GroundedState : PlayerState
{
    public P_GroundedState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
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



        if (xInput != 0 && !player.isBusy)
        {
            stateMachine.Changestate(player.move);
            Debug.Log(xInput);
        }

        if (rb.linearVelocity.x == 0 && xInput == 0)
        {
            stateMachine.Changestate(player.idle);
        }
        
        if (player.jumpBufferCount > 0 && player.isGround)
        {
            
            player.jumpBufferCount = 0;
            stateMachine.Changestate(player.jump);
            Debug.Log("Jump buffered");
        }
        else if (Input.GetButtonDown("Jump") && player.isGround)
        {
            stateMachine.Changestate(player.jump);
            Debug.Log("jumped");
        }

        if (rb.linearVelocity.y < 0)
        {
            stateMachine.Changestate(player.fall);  
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && player.caneWpn)
        {
            stateMachine.Changestate(player.attack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.caneWpn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.caneWpn = false;
        }
      
     
     
    }
}
