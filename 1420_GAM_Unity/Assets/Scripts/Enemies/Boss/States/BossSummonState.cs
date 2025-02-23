using UnityEngine;

public class BossSummonState : BossState
{
    private float tempInterval;

    public BossSummonState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        boss.summonTimer = boss.summonDur;
        boss.transform.position = boss.leftPoint.position;
        tempInterval = 0f;

        if(boss.facingDir == -1)
        {
            boss.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        boss.summonTimer -= Time.deltaTime;
        tempInterval -= Time.deltaTime;

        if(boss.summonTimer < 0f)
        {
            stateMachine.ChangeState(boss.rest);
        }

        if(tempInterval < 0)
        {
            boss.SpawnCrowProjectiles();
            tempInterval = boss.crowProjectileIntervals;
        }

    }
}
