using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    Player player;
    public void Start()
    {
        player = PlayerManager.instance.player;
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
        }
    }


}
