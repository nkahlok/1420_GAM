using Unity.VisualScripting;
using UnityEngine;

public class P_ShieldingState : PlayerState
{
    public P_ShieldingState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;
        player.isShield = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.isBusy = false;
        player.isShield = false;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            stateMachine.Changestate(player.idle);
        }

        if(xInput != 0)
        {
           player.rb.linearVelocity = new Vector2(player.moveSpeed * 0.4f * xInput, rb.linearVelocityY);
        }
        else
        {
            player.SetVelocity(0, rb.linearVelocityY);
        }
    }


}
