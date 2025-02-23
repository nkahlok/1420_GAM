using UnityEngine;

public class BossPlungeState : BossState
{
    public BossPlungeState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        boss.transform.position = new Vector2(boss.player.transform.position.x, boss.topPoint.position.y) ;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        boss.SetVelocity(0, boss.plungeSpeed * -1);
        if(boss.isGround)
        {
            stateMachine.ChangeState(boss.rest);
        }
    }
}
