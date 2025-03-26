//using UnityEditor.Tilemaps;
using UnityEngine;

public class BossDashAttackState : BossState
{
    int hitCount;
    float delayDur;
    public BossDashAttackState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();


        SoundManager.PlaySfx(SoundType.BOSSDASHATK);
        if (boss.attackPatternCount%2 == 0)
        {
            if (boss.facingDir == 1)
                boss.Flip();
            boss.transform.position = boss.rightPoint.position;
            boss.rightPoint.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        else if (boss.attackPatternCount % 2 == 1)
        {
            Debug.Log("odd no.");
            if (boss.facingDir == -1)
                boss.Flip();
            boss.transform.position = boss.leftPoint.position;
            boss.leftPoint.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        boss.canBeCountered = true;

        hitCount = 0;

        boss.dashDust.Play();

        boss.modifier.canBeDamaged = true;

        delayDur = 0.3f;

        sprite.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();

        hitCount = 0;

        boss.canBeCountered = false;

        boss.counterWindow.SetActive(false);

        boss.modifier.canBeDamaged = false;

        boss.dashDust.Stop();

        boss.rightPoint.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        boss.leftPoint.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        boss.counterWindow.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        Debug.Log("Dashing");

        if(delayDur < 0f)
        {
            boss.counterWindow.SetActive(true);
            sprite.enabled = true;
            boss.SetVelocity(boss.dashAttackSpeed * boss.facingDir, rb.linearVelocityY);
        }


        /*if (Vector2.Distance(boss.player.transform.position, boss.transform.position) < 5)
        {
            boss.counterWindow.SetActive(true);
        }
        else
        {
            
        }*/


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

        delayDur -= Time.deltaTime; 
    }
}
