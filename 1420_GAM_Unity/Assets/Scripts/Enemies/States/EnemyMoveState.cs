using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBool) : base(_enemy, _enemyStateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
       

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(enemy.wasAttacked == true)
        {

            if((player.transform.position.x > enemy.transform.position.x && enemy.facingDir == -1)|| (player.transform.position.x < enemy.transform.position.x && enemy.facingDir == 1))
            {
                enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir *-1, rb.linearVelocity.y);
                enemy.wasAttacked = false;
            }
   
        }
        else
        {
            enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocity.y);
        }

        if(enemy.isWall || !enemy.isGround)
        {
 
            enemyStateMachine.Changestate(enemy.enemyIdleState);
        }

        //Debug.Log("Am moving");

        if (enemy.isCat)
            CatUpdate();
        else if (enemy.isRat)
            RatUpdate();


    
    }

    public void CatUpdate()
    {
        if (enemy.isPlayer || Vector2.Distance(player.transform.position, enemy.transform.position) < enemy.playerAttackDistance * 4)
        {
            enemyStateMachine.Changestate(enemy.enemyAggroState);
        }
    }

    public void RatUpdate() 
    { 
        if(enemy.isPlayer || Vector2.Distance(player.transform.position, enemy.transform.position) < 2)
        {

            enemyStateMachine.Changestate(enemy.enemyAggroState);
          
        }
    }

}
