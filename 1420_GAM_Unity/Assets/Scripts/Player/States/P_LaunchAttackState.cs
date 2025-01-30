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
        comboCounter = 0;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
     

        if (SkillManager.instance.launchSkill.launchUp == true)
        {
            player.SetVelocity(SkillManager.instance.launchSkill.launchVelocity[0].x, SkillManager.instance.launchSkill.launchVelocity[0].y);

            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<Enemy>() != null)
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    enemy.KnockBack("Launch Up");
                    enemy.airlock = true;   
                    enemy.enemyStateMachine.Changestate(enemy.enemyKnockDownState);
                   

                }
            }
        }
        else if (SkillManager.instance.launchSkill.launchDown == true)
        {

            player.SetVelocity(SkillManager.instance.launchSkill.launchVelocity[1].x, SkillManager.instance.launchSkill.launchVelocity[1].y * -1);

            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<Enemy>() != null)
                {
                    collider.GetComponent<Enemy>().KnockBack("Launch Down");
                }
            }
        }
        else
        {
            player.SetVelocity(SkillManager.instance.launchSkill.launchVelocity[2].x*player.facingDir, SkillManager.instance.launchSkill.launchVelocity[2].y);

            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<Enemy>() != null)
                {
                    collider.GetComponent<Enemy>().KnockBack("Forward");
                }
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
        foreach(Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                collider.GetComponent<Enemy>().AirlockCoroutine();
            }
        }
    }

    public override void Update()
    {
        base.Update();


      
    }
}
