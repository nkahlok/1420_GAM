using UnityEngine;

public class BossRestState : BossState
{
    

    public BossRestState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sprite.color = new Color(1f, 1f, 1f, 0f);
        boss.transform.position = boss.restPoint.position;
        boss.restTimer = boss.restDur;
        boss.attackPatternCount++;
    }

    public override void Exit()
    {
        base.Exit();
        sprite.color = new Color(1f, 1f, 1f, 1f);
        Debug.Log("Exited rest");
    }

    public override void Update()
    {
        base.Update();

        boss.restTimer -= Time.deltaTime;

        if (boss.restTimer < 0f)
        {
            if(boss.attackPatternCount == 0 || boss.attackPatternCount == 3)
                stateMachine.ChangeState(boss.dashAttack);

            else if (boss.attackPatternCount == 1)
                stateMachine.ChangeState(boss.plunge);
            
            else if(boss.attackPatternCount == 2)
                stateMachine.ChangeState(boss.summon);
        }
    }
}
