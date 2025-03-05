using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    Player player;
    ManHoleSkill skill1;
    public void Start()
    {
        player = PlayerManager.instance.player;
        skill1 = SkillManager.instance.manholeSkill;
        
    }

    public void EndAttack()
    {
        player.stateMachine.Changestate(player.fall);
        player.anim.SetBool("Throw", false);
        player.isBusy = false;

    }

    public void Attack()
    {
        ComboHardCode();

        /*if(player.facingDir == 1)
        {
            player.slashEffectRight.Play();
        }
        else if (player.facingDir == -1)
        {
            player.slashEffectLeft.Play();
        }*/

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
        foreach (Collider2D collider in colliders)
        {
            if(collider.GetComponent<EnemyTypeModifier>() != null)
            {
                collider.GetComponent<EnemyTypeModifier>().Damage(1);
                player.comboHitCount = player.comboTime;
                player.hitEffect.Play();
                player.mainCam.GetComponentInChildren<Animator>().SetTrigger("Shake");
            }

            if (collider.GetComponent<Enemy>() != null)
            {
                collider.GetComponent<Enemy>().damaged = true;
            }

        }        
    }

    public void NormalHitStop()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null) 
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.HitStop(player.normalHitStop);
            }
        }
    }

    public void KnockUpHitStop()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.HitStop(player.knockUpHitStop);
            }
        }
    }

    public void ForwardHitStop()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.HitStop(player.forwardHitStop);
            }
        }
    }


    public void KnockBack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null && player.isGround)
            {
                collider.GetComponent<Enemy>().KnockBack("Normal");

            }
        }
    }

    public void IFrame()
    {
        StartCoroutine("IFrameCorountine", 0.5f);
    }

    IEnumerator IFrameCorountine(float seconds)
    {
        player.canBeDamaged = false;
        yield return new WaitForSecondsRealtime(seconds);
        player.canBeDamaged = true;
    }
 

    public void ComboHardCode()
    {
        if(player.comboCounter == 0)
        {
            player.comboCounter = 1;
        }
        else if(player.comboCounter == 1)
        {
            player.comboCounter = 2;
        }
        else if(player.comboCounter == 2)
        {
            player.comboCounter = 0;
        }
    }
}
