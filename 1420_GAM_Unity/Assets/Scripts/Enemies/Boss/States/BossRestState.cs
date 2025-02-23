using UnityEngine;

public class BossRestState : BossState
{
   

    public BossRestState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sprite.color = new Color(1f, 1f, 1f, 0f);
        boss.transform.position = boss.restPoint.position;
        boss.restTimer = boss.restDur;
        boss.attackPatternCount++;
        boss.counterWindow.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
        sprite.color = new Color(1f, 1f, 1f, 1f);
        Debug.Log("Exited rest");
        

    }

    public override void Update()
    {
        base.Update();

        boss.restTimer -= Time.deltaTime;

        if (boss.restTimer < 0f)
        {
            if(boss.attackPatternCount == boss.dashAttackPatternNumber[0] || boss.attackPatternCount == boss.dashAttackPatternNumber[1])
            {
                

                stateMachine.ChangeState(boss.dashAttack);

               
            }

            else if (boss.attackPatternCount == boss.plungeAttackPatternNumber[0])
            {
                

                stateMachine.ChangeState(boss.plunge);

            
            }
            
            else if(boss.attackPatternCount == boss.summonAttackPatternNumber[0])
            {
                
                stateMachine.ChangeState(boss.summon);
       
            }
        }
    }
}
