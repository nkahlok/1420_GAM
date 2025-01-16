using Unity.VisualScripting;
using UnityEngine;

public class P_ManHoleAimingState : PlayerState
{
    public float xDir;
    public float yDir;
    public float xThrow;
    public float yThrow;
    protected GameObject indicator;
    protected bool keyDown;

    public P_ManHoleAimingState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;
        indicator = player.manholeIndicator;
        indicator.SetActive(false);
        keyDown = true;

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

        indicator.SetActive(false);


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
            keyDown = false;
            indicator.SetActive(false);
            stateMachine.Changestate(player.idle);           
            player.isBusy = false;
        }

        player.FlipControl(xInput);

        IndicatorRotate();

        if ((xInput != 0 && keyDown) || (yInput != 0 && keyDown))
        {
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);
        }

    }

    protected void IndicatorRotate()
    {
        if (xInput == 0 && yInput == 1)
        {
            indicator.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (xInput == 0 && yInput == -1)
        {
            indicator.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (xInput == 1 && yInput == 1)
        {
            indicator.transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (xInput == 1 && yInput == -1)
        {
            indicator.transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        else if (xInput == 1 && yInput == 0)
        {
            indicator.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (xInput == -1 && yInput == 1)
        {
            indicator.transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (xInput == -1 && yInput == 0)
        {
            indicator.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (xInput == -1 && yInput == -1)
        {
            indicator.transform.rotation = Quaternion.Euler(0, 0, 225);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(player.manholeThrowChecker.position.x, player.manholeThrowChecker.position.y), new Vector2(player.manholeThrowChecker.position.x + player.manHoleThrowRange * xDir, player.manholeThrowChecker.position.y + player.manHoleThrowRange * yDir));
    }
}
