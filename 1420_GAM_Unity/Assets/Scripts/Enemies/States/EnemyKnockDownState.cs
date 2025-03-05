using UnityEngine;

public class EnemyKnockDownState : EnemyState
{
    private float stateDur;
    private float stayKnockedDown;

    public EnemyKnockDownState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBool) : base(_enemy, _enemyStateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("knocked up");
        stateDur = enemy.airborneTime - 0.1f;
        stayKnockedDown = enemy.airborneTime/2;
        enemy.knockedDown = false;
        enemy.counterWindowImg.SetActive(false);

        if(enemy.countered)
            rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[3].x * enemy.facingDir * -1, SkillManager.instance.launchSkill.launchVelocity[3].y);
        
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("out");
        enemy.damaged = true;     
        enemy.launchDown = false;   
        enemy.countered = false;
        //rb.gravityScale = 3;

    }

    public override void Update()
    {
        base.Update();


        //anti-gravity code if not using bounce mechanic
       /* if(stateDur < 0)
        {

            rb.gravityScale = 0;
            enemy.SetVelocity(0, 0);
        }

        if(enemy.airborneCount < 0)
        {
            if (enemy.launchDown)
            {
                rb.gravityScale = 3;
                rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[1].x, SkillManager.instance.launchSkill.launchVelocity[1].y * -1);
            }
            else if (!enemy.launchDown)
            {
                rb.gravityScale = 3;
                enemy.SetVelocity(0, -8);
            }
        }*/

        if (enemy.countered && enemy.isGround || rb.linearVelocityY == 0)
        {
            stayKnockedDown -= Time.deltaTime;
        }

        if(enemy.isGround && stateDur < 0 && !enemy.countered || stayKnockedDown < 0)
        {

            enemy.enemyStateMachine.Changestate(enemy.enemyMoveState); 
        }
        
        stateDur -= Time.deltaTime; 
    }
}
