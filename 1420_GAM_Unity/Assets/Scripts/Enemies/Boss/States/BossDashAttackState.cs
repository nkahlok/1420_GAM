using UnityEditor.Tilemaps;
using UnityEngine;

public class BossDashAttackState : BossState
{
    public BossDashAttackState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(boss.attackPatternCount%2 == 0)
        {
            if (boss.facingDir == 1)
                boss.Flip();
            boss.transform.position = boss.rightPoint.position;
        }

        else if (boss.attackPatternCount % 2 == 1)
        {
            Debug.Log("odd no.");
            if (boss.facingDir == -1)
                boss.Flip();
            boss.transform.position = boss.leftPoint.position;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Debug.Log("Dashing");

        boss.SetVelocity(boss.dashAttackSpeed * boss.facingDir, rb.linearVelocityY);
    }
}
