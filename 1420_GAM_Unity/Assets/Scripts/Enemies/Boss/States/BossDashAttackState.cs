using UnityEditor.Tilemaps;
using UnityEngine;

public class BossDashAttackState : BossState
{
    protected int hitCount;
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

        boss.canBeCountered = true;

        hitCount = 0;
    }

    public override void Exit()
    {
        base.Exit();

        hitCount = 0;

        boss.canBeCountered = false;
    }

    public override void Update()
    {
        base.Update();

        Debug.Log("Dashing");

        boss.SetVelocity(boss.dashAttackSpeed * boss.facingDir, rb.linearVelocityY);

        if (Vector2.Distance(boss.player.transform.position, boss.transform.position) < 5)
        {
            boss.counterWindow.SetActive(true);
        }
        else
        {
            boss.counterWindow.SetActive(false);
        }


        Collider2D[] colliders = Physics2D.OverlapCircleAll(boss.dashAttackChecker.position, boss.dashAttackRange);

        foreach (Collider2D collider in colliders)
        {
            if(collider.GetComponent<Player>() != null) 
            { 
                if(hitCount == 0)
                {
                    boss.player.Damage(boss.dashAttackDamage);
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


    }
}
