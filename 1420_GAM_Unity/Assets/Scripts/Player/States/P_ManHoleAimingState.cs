using Unity.VisualScripting;
using UnityEngine;

public class P_ManHoleAimingState : PlayerState
{
    public float xDir;
    public float yDir;
    public float xThrow;
    public float yThrow;
    public P_ManHoleAimingState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;   
    }

    public override void Exit()
    {
        base.Exit();

        if(xInput == 1)
        {
            xThrow = 1;
        }
        else if (xInput == -1)
        {
            xThrow = -1;
        }
        else if (xInput == 0)
        {
            xThrow = 0;
        }

        if (yInput == 1)
        {
            yThrow = 1;
        }
        else if (yInput == -1)
        {
            yThrow = -1;
        }
        else if (yInput == 0)
        {
            yThrow = 0;
        }

           
 
    }

    public override void Update()
    {
        base.Update();
        xDir = xInput;
        yDir = yInput;

        player.SetVelocity(0, 0);

        if (Input.GetKeyUp(KeyCode.Mouse0) && (xDir!=0||yDir!=0))
        {

            SkillManager.instance.skill1.ThrowManHole();
            anim.SetBool("Throw", true);
            stateMachine.Changestate(player.idle);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            stateMachine.Changestate(player.idle);
            player.isBusy = false;
        }

        player.FlipControl(xInput);
       

     
 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(player.manholeThrowChecker.position.x, player.manholeThrowChecker.position.y), new Vector2(player.manholeThrowChecker.position.x + player.manHoleThrowRange * xDir, player.manholeThrowChecker.position.y + player.manHoleThrowRange * yDir));
    }
}
