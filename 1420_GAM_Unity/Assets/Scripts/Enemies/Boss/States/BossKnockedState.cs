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
        knockedDur = boss.knockedDownDur;
        boss.canBeAerialKnocked = true;
        boss.knockedDown = false;

        //boss.SetVelocity(0, 0);

        boss.modifier.canBeDamaged = true;

        if (boss.countered)
        {
            rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[3].x * boss.facingDir * -1, SkillManager.instance.launchSkill.launchVelocity[3].y);
            boss.modifier.Damage(1);
        }
       
        
    }

    public override void Exit()
    {
        base.Exit();
        boss.countered = false;
        boss.modifier.canBeDamaged = false;
        boss.canBeAerialKnocked = false;
    }

    public override void Update()
    {
        base.Update();
        
        knockedDur -= Time.deltaTime;   

        if(knockedDur < 0 && !boss.isTired)
        {
            stateMachine.ChangeState(boss.rest);
        }

        if (boss.isTired && boss.isGround && knockedDur < 0)
        {
            stateMachine.ChangeState(boss.tired);
        }
    }
}
