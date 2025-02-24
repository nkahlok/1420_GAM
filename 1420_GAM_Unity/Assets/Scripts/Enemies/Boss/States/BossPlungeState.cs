using UnityEngine;

public class BossPlungeState : BossState
{
    int hitCount;

    public BossPlungeState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        boss.transform.position = new Vector2(boss.player.transform.position.x, boss.topPoint.position.y);
        hitCount = 0;
        boss.canBeCountered = true;
        boss.modifier.canBeDamaged = true;

    }

    public override void Exit()
    {
        base.Exit();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(boss.plungeAttackChecker.position, boss.plungeAttackRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Player>() != null)
            {
                if (hitCount == 0)
                {
                    boss.player.Damage(boss.plungeAttackDamage);
                    hitCount++;
                    if (boss.transform.position.x > boss.player.transform.position.x && boss.player.facingDir == -1)
                    {
                        boss.player.KnockBack(-1 * 1, 1);
                    }
                    else if (boss.transform.position.x < boss.player.transform.position.x && boss.player.facingDir == 1)
                    {
                        boss.player.KnockBack(-1 * 1, 1);
                    }
                    else
                    {
                        boss.player.KnockBack(1, 1);
                    }
                }


            }
        }
        hitCount = 0;
        boss.canBeCountered = false;
        boss.modifier.canBeDamaged = false;
        boss.counterWindow.SetActive(false);
    }

    public override void Update()
    {
        base.Update();


        boss.SetVelocity(0, boss.plungeSpeed * -1);

       

        if (boss.isGround)
        {
            if(boss.phaseTwo)
                boss.SpawnShockwave();

            stateMachine.ChangeState(boss.rest);
        }

        if (Vector2.Distance(boss.player.transform.position, boss.transform.position) < 5)
        {
            boss.counterWindow.SetActive(true);
        }
        else
        {
            boss.counterWindow.SetActive(false);
        }

        

    }
}
