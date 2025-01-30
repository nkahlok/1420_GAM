using UnityEngine;

public class EnemyKnockDownState : EnemyState
{
    public EnemyKnockDownState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBool) : base(_enemy, _enemyStateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("knocked up");
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("out");
    }

    public override void Update()
    {
        base.Update();

        if (!enemy.airlock)
        {

            //enemy.SetVelocity(20, 20);
            rb.gravityScale = 3;

        }
        else
        {
            enemy.SetVelocity(0, 0);    
            rb.gravityScale = 0;
        }


    }
}
