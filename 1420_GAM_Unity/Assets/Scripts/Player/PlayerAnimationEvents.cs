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
        player.stateMachine.Changestate(player.idle);
        player.anim.SetBool("Throw", false);
        Debug.Log("Attack ended");
        player.isBusy = false;  
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);
        foreach (Collider2D collider in colliders)
        {
            if(collider.GetComponent<EnemyTypeModifier>() != null)
            {
                collider.GetComponent<EnemyTypeModifier>().Damage(1);
            }

            /*if(collider.GetComponent<ManHolePhysics>() != null)
            {
                Debug.Log("Hit manhole");
                if (!collider.GetComponent<ManHolePhysics>().canHitEnemy)
                {
                     collider.GetComponent<ManHolePhysics>().Bounce();

                }
            }*/

        }

        
    }


}
