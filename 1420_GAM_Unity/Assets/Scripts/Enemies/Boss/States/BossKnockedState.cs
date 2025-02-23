using UnityEngine;

public class BossKnockedState : BossState
{
    float knockedDur;

    public BossKnockedState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        knockedDur = 0.5f;
        boss.knockedDown = false;

        if (boss.countered)
            rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[3].x * boss.facingDir * -1, SkillManager.instance.launchSkill.launchVelocity[3].y);
    }

    public override void Exit()
    {
        base.Exit();
        boss.countered = false;
    }

    public override void Update()
    {
        base.Update();
        
        knockedDur -= Time.deltaTime;   

        if(knockedDur < 0)
        {
            stateMachine.ChangeState(boss.rest);
        }
    }
}
