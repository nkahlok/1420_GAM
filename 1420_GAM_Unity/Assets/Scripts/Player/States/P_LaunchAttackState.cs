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
       

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
     

        if (SkillManager.instance.launchSkill.launchUp == true)
        {
            player.SetVelocity(SkillManager.instance.launchSkill.launchVelocity[0].x, SkillManager.instance.launchSkill.launchVelocity[0].y);
            anim.SetBool("LaunchUp", true);
            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<Enemy>() != null)
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    enemy.isBusy = false;
                    
                    //enemy.enemyStateMachine.Changestate(enemy.enemyKnockDownState);
                    //Debug.Log("Knocked dis bitch up");

                    if ((enemy.canBeCountered))
                    {
                        player.comboHitCount = player.comboTime;
                        player.hitEffect.Play();
                        enemy.KnockBack("Countered");
                        enemy.canBeCountered = false;
                    }
                    else
                    {
                        enemy.KnockBack("Launch Up");
                        enemy.airborneCount = enemy.airborneTime;
                        enemy.knockedDown = true;
                    }

                }
                else if(collider.GetComponent<Boss>() != null)
                {
                    Boss boss = collider.GetComponent<Boss>();
                    boss.isBusy = false;
                    

                    if ((boss.canBeCountered))
                    {
                        player.comboHitCount = player.comboTime;
                        player.hitEffect.Play();
                        boss.KnockBack("Countered");
                        boss.canBeCountered = false;
                    }
                    else
                    {
                        boss.KnockBack("Launch Up");
                        if (boss.isTired)
                            boss.knockedDown = true;
                    }

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
        else if(SkillManager.instance.launchSkill.jabAttack == true)
        {
            player.SetVelocity(SkillManager.instance.launchSkill.launchVelocity[2].x*player.facingDir, SkillManager.instance.launchSkill.launchVelocity[2].y);

            SkillManager.instance.launchSkill.jabAttack = false;

            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<Enemy>() != null)
                {

                    Enemy enemy = collider.GetComponent<Enemy>();



                    if (enemy.canBeCountered && collider.GetComponent<EnemyTypeModifier>() != null)
                    {

                        collider.GetComponent<EnemyTypeModifier>().Damage(1);
                        player.comboHitCount = player.comboTime;
                        player.hitEffect.Play();

                        enemy.KnockBack("Countered");
                        enemy.canBeCountered = false;
                        return;
                    }
                    else
                    {
                        enemy.KnockBack("Forward");
                    }
                }
                else if (collider.GetComponent<Boss>() != null)
                {
                    Boss boss = collider.GetComponent<Boss>();

                    if ((boss.canBeCountered))
                    {
                        player.comboHitCount = player.comboTime;
                        player.hitEffect.Play();
                        boss.KnockBack("Countered");
                        boss.canBeCountered = false;
                    }

                }
            }
        }

    }

    public override void Exit()
    {
        base.Exit();

        

        anim.SetBool("LaunchUp", false);

        //put this here just incase need to use
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
        foreach(Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {

            }
        }
    }

    public override void Update()
    {
        base.Update();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Boss>() != null)
            {
                if (SkillManager.instance.launchSkill.launchUp == true)
                {
                    Boss boss = collider.GetComponent<Boss>();
                    boss.isBusy = false;


                    if ((boss.canBeCountered))
                    {
                        player.comboHitCount = player.comboTime;
                        player.hitEffect.Play();
                        boss.KnockBack("Countered");
                        boss.canBeCountered = false;
                    }

                }
            }
        }

    }
}
