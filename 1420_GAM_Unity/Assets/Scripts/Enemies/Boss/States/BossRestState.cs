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
        
        boss.counterWindow.SetActive(false);
        boss.attackPatternCount++;
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
            if(boss.phaseOne)
                PhaseOneAttackPattern(); 
            if(boss.phaseTwo)
                PhaseTwoAttackPattern();
            if(boss.phaseThree)
                PhaseThreeAttackPattern();
        }
    }

    public void PhaseOneAttackPattern()
    {
        if (boss.attackPatternCount == boss.dashAttackPatternNumberOne[0] || boss.attackPatternCount == boss.dashAttackPatternNumberOne[1])
        {


            stateMachine.ChangeState(boss.dashAttack);

            
        }

        else if (boss.attackPatternCount == boss.plungeAttackPatternNumberOne[0])
        {


            stateMachine.ChangeState(boss.plunge);

          
        }

        else if (boss.attackPatternCount == boss.summonAttackPatternNumberOne[0])
        {

            stateMachine.ChangeState(boss.summon);
          
        }

    }

    public void PhaseTwoAttackPattern()
    {
        if (boss.attackPatternCount == boss.dashAttackPatternNumberTwo[0])
        {


            stateMachine.ChangeState(boss.dashAttack);
         

        }

        else if (boss.attackPatternCount == boss.plungeAttackPatternNumberTwo[0] || boss.attackPatternCount == boss.plungeAttackPatternNumberTwo[1])
        {


            stateMachine.ChangeState(boss.plunge);
            

        }

        else if (boss.attackPatternCount == boss.summonAttackPatternNumberTwo[0])
        {

            stateMachine.ChangeState(boss.summon);
           
        }
    }

    public void PhaseThreeAttackPattern()
    {
        if (boss.attackPatternCount == boss.dashAttackPatternNumberThree[0])
        {


            stateMachine.ChangeState(boss.dashAttack);
           

        }

        else if (boss.attackPatternCount == boss.plungeAttackPatternNumberThree[0])
        {


            stateMachine.ChangeState(boss.plunge);
            

        }

        else if (boss.attackPatternCount == boss.summonAttackPatternNumberThree[0] || boss.attackPatternCount == boss.summonAttackPatternNumberThree[1])
        {

            stateMachine.ChangeState(boss.summon);
            
        }
    }

}
