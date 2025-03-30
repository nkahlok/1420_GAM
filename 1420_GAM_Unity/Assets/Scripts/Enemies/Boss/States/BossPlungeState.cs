using Unity.VisualScripting;
using UnityEngine;

public class BossPlungeState : BossState
{
    int hitCount;
    float delayDur;

    public BossPlungeState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.gravityScale = 0f;
        SoundManager.PlaySfx(SoundType.BOSSDASHATK);
        boss.topPoint.position = new Vector2(boss.player.transform.position.x, boss.topPoint.position.y);
        boss.transform.position = new Vector2(boss.player.transform.position.x, boss.topPoint.position.y);
        boss.topPoint.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        boss.topPoint.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        boss.indicators[2].SetActive(true);
        hitCount = 0;
        boss.canBeCountered = true;
        boss.modifier.canBeDamaged = true;

        sprite.enabled = false;

        delayDur = 1f;
        


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

        if(delayDur < 0f)
        {
            rb.gravityScale = 3f;
            boss.counterWindow.SetActive(true);
            boss.topPoint.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            boss.topPoint.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            boss.indicators[2].SetActive(false);
            sprite.enabled = true;
            boss.SetVelocity(0, boss.plungeSpeed * -1);
        }

       

        if (boss.isGround)
        {
            if(boss.phaseTwo || boss.phaseThree)
                boss.SpawnShockwave();

            stateMachine.ChangeState(boss.rest);
        }

       /* if (Vector2.Distance(boss.player.transform.position, boss.transform.position) < 5)
        {
            
        }
        else
        {
            boss.counterWindow.SetActive(false);
        }*/

        delayDur -= Time.deltaTime;

    }
}
