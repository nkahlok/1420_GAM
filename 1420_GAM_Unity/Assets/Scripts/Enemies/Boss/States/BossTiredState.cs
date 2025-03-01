using UnityEngine;

public class BossTiredState : BossState
{
    public BossTiredState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        boss.isTired = true;
        boss.tiredTriggered = true;
        boss.modifier.canBeDamaged = true;
        boss.transform.position = boss.tiredPoint.position;
        boss.SetVelocity(0,0);
        
    }

    public override void Exit()
    {
        base.Exit();
        boss.modifier.canBeDamaged = false;
        boss.SetVelocity(rb.linearVelocityX, rb.linearVelocityY);
        
    }

    public override void Update()
    {
        
        base.Update();
    }
}
