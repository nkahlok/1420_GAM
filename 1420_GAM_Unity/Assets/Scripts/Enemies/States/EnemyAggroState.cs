//using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAggroState : EnemyState
{
    private int moveDir;
    public EnemyAggroState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBool) : base(_enemy, _enemyStateMachine, _animBool)
    {

    }

    public override void Enter()
    {
        base.Enter();
        enemy.aggroCount = enemy.aggroDur;
        enemy.aggroImg.SetActive(true);
        SoundManager.PlaySfx(SoundType.ENEMYAGGRO);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.aggroImg.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if(enemy.isCat)
            CatUpdate();
        else if(enemy.isRat)
            RatUpdate();


    }

    protected void CatUpdate()
    {
        //move direction changes based on where the player is, moving them left and right
        if (player.transform.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.transform.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }

        if (player.transform.position.y > enemy.transform.position.y && enemy.isPlayerAbove)
        {
            moveDir = 0;
        }

        if (!enemy.isPlayer)
        {
            //Debug.Log("I do not detect anyone");

            enemy.aggroCount -= Time.deltaTime;

            if (enemy.isWall)
            {
                Debug.Log("I detect wall");
                enemy.SetVelocity(0, rb.linearVelocityY);
                if ((player.transform.position.x > enemy.transform.position.x && enemy.facingDir == -1) || (player.transform.position.x < enemy.transform.position.x && enemy.facingDir == 1))
                {
                    enemy.Flip();
                }
            }

            if (enemy.aggroCount < 0)
            {
                enemyStateMachine.Changestate(enemy.enemyIdleState);
            }
        }
        else if (enemy.isPlayer)
        {
            enemy.aggroCount = enemy.aggroDur;
        }

        enemy.attackCount -= Time.deltaTime;

        if (!enemy.isWall)
        {
            enemy.SetVelocity(enemy.moveSpeed * 1.5f * moveDir, rb.linearVelocity.y);
        }
        if (!enemy.isGround)
        {
            enemyStateMachine.Changestate(enemy.enemyIdleState);
        }

      
        if (enemy.ray.distance <= enemy.playerAttackDistance && enemy.isPlayer && enemy.attackCount < 0)
        {
            enemyStateMachine.Changestate(enemy.enemyAttackState);
            
        }
        else if (enemy.ray.distance <= enemy.playerAttackDistance && enemy.isPlayer || Vector2.Distance(player.transform.position, enemy.transform.position) < enemy.playerAttackDistance)
        {
            enemy.SetVelocity(0, rb.linearVelocity.y);
        }

    }

    protected void RatUpdate()
    {
        if (player.transform.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.transform.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }

        if (player.transform.position.y > enemy.transform.position.y && enemy.isPlayerAbove)
        {
            moveDir = 0;
        }

        if ((player.transform.position.x > enemy.transform.position.x && enemy.facingDir == -1) || (player.transform.position.x < enemy.transform.position.x && enemy.facingDir == 1))
        {
            enemy.Flip();
        }     

        if (enemy.ray.distance <= enemy.playerAttackDistance && enemy.isPlayer)
        {
            enemyStateMachine.Changestate(enemy.enemyAttackState);
        }


        if (!enemy.isPlayer && !enemy.isPlayerAbove)
        {
            enemyStateMachine.Changestate(enemy.enemyMoveState);
        }

        if (!enemy.isGround)
        {
            enemy.Flip();
            enemyStateMachine.Changestate(enemy.enemyMoveState);
        }

        if (!enemy.isPlayer)
        {
            //Debug.Log("I do not detect anyone");

            enemy.aggroCount -= Time.deltaTime;

            if (enemy.isWall)
            {
                Debug.Log("I detect wall");
                enemy.SetVelocity(0, rb.linearVelocityY);
                if ((player.transform.position.x > enemy.transform.position.x && enemy.facingDir == -1) || (player.transform.position.x < enemy.transform.position.x && enemy.facingDir == 1))
                {
                    enemy.Flip();
                }
            }

            if (enemy.aggroCount < 0)
            {
                enemyStateMachine.Changestate(enemy.enemyIdleState);
            }
        }
        else if (enemy.isPlayer)
        {
            enemy.aggroCount = enemy.aggroDur;
        }

    }

}
