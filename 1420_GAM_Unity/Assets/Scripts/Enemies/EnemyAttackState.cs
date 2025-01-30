using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float stateDur;
    private float stateCount;

    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBool) : base(_enemy, _enemyStateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateDur = 0.5f;
        stateCount = stateDur;
        //Debug.Log("attack executed");
    }

    public override void Exit()
    {
        base.Exit();
        enemy.attackCount = enemy.attackCD;
        //Debug.Log("attack finish");
    }

    public override void Update()
    {
        base.Update();
        stateCount -= Time.deltaTime;
        if(stateCount < 0 )
        {
            enemyStateMachine.Changestate(enemy.enemyAggroState);
        }

        enemy.SetVelocity(0, rb.linearVelocity.y);
    }
}
