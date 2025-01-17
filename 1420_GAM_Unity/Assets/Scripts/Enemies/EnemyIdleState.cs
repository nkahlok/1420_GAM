using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBool) : base(_enemy, _enemyStateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.idleCount = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Flip();
    }

    public override void Update()
    {
        enemy.SetVelocity(0, rb.linearVelocity.y);
        base.Update();
        if (enemy.idleCount < 0)
        {
            enemyStateMachine.Changestate(enemy.enemyMoveState);
        }

        if (enemy.isPlayer || Vector2.Distance(player.transform.position, enemy.transform.position) < enemy.playerAttackDistance)
        {
            enemyStateMachine.Changestate(enemy.enemyAggroState);
        }
    }
}
