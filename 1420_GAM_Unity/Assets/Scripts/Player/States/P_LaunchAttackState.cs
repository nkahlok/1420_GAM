using UnityEngine;

public class P_LaunchAttackState : PlayerState
{
    

    public P_LaunchAttackState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;

        if (SkillManager.instance.launchSkill.launchUp == true)
        {
            player.SetVelocity(SkillManager.instance.launchSkill.launchVelocity[0].x, SkillManager.instance.launchSkill.launchVelocity[0].y);

        }
        else if (SkillManager.instance.launchSkill.launchDown == true)
        {

            player.SetVelocity(SkillManager.instance.launchSkill.launchVelocity[1].x, SkillManager.instance.launchSkill.launchVelocity[1].y * -1);
        }
        else
        {
            player.SetVelocity(SkillManager.instance.launchSkill.launchVelocity[2].x*player.facingDir, SkillManager.instance.launchSkill.launchVelocity[2].y);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


      
    }
}
