using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float stateDur;
    private float stateCount;
    private float bulletIntervals;

    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBool) : base(_enemy, _enemyStateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateDur = 3f;
        bulletIntervals = -1;

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
        stateDur -= Time.deltaTime;
        bulletIntervals -= Time.deltaTime;
        /*if(stateCount < 0 )
        {
            enemyStateMachine.Changestate(enemy.enemyAggroState);
        }*/

        enemy.SetVelocity(0, rb.linearVelocity.y);

        //above are codes for rat and cat, below is just rat
        if (enemy.isRat)
            RatUpdate();
     
    }

    protected void RatUpdate()
    {
        if (enemy.isRat)
        {
            if ((player.transform.position.x > enemy.transform.position.x && enemy.facingDir == -1) || (player.transform.position.x < enemy.transform.position.x && enemy.facingDir == 1))
            {
                enemy.Flip();
            }
        }

        if (enemy.isRat && stateDur < 0)
        {
            enemy.enemyStateMachine.Changestate(enemy.ratEnemyReloadState);
        }

        if (enemy.isRat && bulletIntervals < 0)
        {
            bulletIntervals = 0.5f;
            enemy.SpawnBullets();
        }

    }
}
