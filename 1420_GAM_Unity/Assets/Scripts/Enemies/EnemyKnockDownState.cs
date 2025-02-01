using UnityEngine;

public class EnemyKnockDownState : EnemyState
{
    private float stateDur;


    public EnemyKnockDownState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBool) : base(_enemy, _enemyStateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("knocked up");
        rb.gravityScale = 0;
        stateDur = enemy.airborneTime;
        //enemy.gameObject.transform.SetParent(player.gameObject.transform);


    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("out");
     
    }

    public override void Update()
    {
        base.Update();

       /* if (!enemy.airlock)
        {

            rb.gravityScale = 3;

        }
        else
        {
            enemy.SetVelocity(0, 0);    
            rb.gravityScale = 0;
        }

        if(!enemy.airlock && enemy.isGround && rb.linearVelocityY == 0)
        {
            enemyStateMachine.Changestate(enemy.enemyAggroState);
        }

        Debug.Log("In state"); 
       */


        if(enemy.airborneCount < 0)
        {
            rb.gravityScale = 3;
        }

        if(enemy.isGround && stateDur < 0)
        {
            //enemy.gameObject.transform.SetParent(null);
            wasAttacked = true;
            enemy.enemyStateMachine.Changestate(enemy.enemyMoveState); 
        }
        
        stateDur -= Time.deltaTime; 
    }
}
