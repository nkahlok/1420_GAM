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

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocity.y);

        if(enemy.isWall || !enemy.isGround)
        {
            enemyStateMachine.Changestate(enemy.enemyIdleState);
        }

        Debug.Log("Am moving");

        if(enemy.isPlayer)
        {
            enemyStateMachine.Changestate(enemy.enemyAggroState);
        }
    
    }
}
