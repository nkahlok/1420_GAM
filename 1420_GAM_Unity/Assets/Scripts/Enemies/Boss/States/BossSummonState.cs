using UnityEngine;

public class BossSummonState : BossState
{
    private float tempInterval;
    private float tempDur;

    public BossSummonState(Boss _boss, BossStateMachine _stateMachine, string _animBool) : base(_boss, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        boss.counterWindow.SetActive(false);
        boss.summonTimer = boss.summonDur;
        boss.transform.position = boss.leftPoint.position;
        tempInterval = 0f;

        if (boss.facingDir == -1)
        {
            boss.Flip();
        }

        if (boss.phaseThree)
        {
            tempDur = boss.summonDur / 2;

            if (boss.attackPatternCount % 2 == 0)
            {
                if (boss.facingDir == 1)
                    boss.Flip();
                boss.transform.position = boss.rightPoint.position;
            }

            else if (boss.attackPatternCount % 2 == 1)
            {
                if (boss.facingDir == -1)
                    boss.Flip();
                boss.transform.position = boss.leftPoint.position;
            }

            boss.SpawnBirdWall();
        }

        boss.modifier.canBeDamaged = true;
    }

    public override void Exit()
    {
        base.Exit();

        boss.modifier.canBeDamaged = false;

        for(int i = 0; i < boss.crowSpawners.Length; i++) 
        {
            boss.crowSpawners[i].gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public override void Update()
    {
        base.Update();

        boss.summonTimer -= Time.deltaTime;
        tempInterval -= Time.deltaTime;
        tempDur -= Time.deltaTime;  

        if(boss.phaseOne || boss.phaseTwo)
        {
            if (boss.summonTimer < 0f)
            {
                stateMachine.ChangeState(boss.rest);
            }
        }

        if (boss.phaseThree)
        {
            if (tempDur < 0f)
            {
                stateMachine.ChangeState(boss.rest);
            }
        }
      

        if(boss.phaseOne || boss.phaseTwo)
        {
            if (tempInterval < 0)
            {
                boss.SpawnCrowProjectiles();

                if (boss.phaseOne)
                    tempInterval = boss.crowProjectileIntervals;
                else if (boss.phaseTwo)
                    tempInterval = boss.crowProjectileIntervals / 2;
            }
        }

        //sprite.color = new Color(1f, 1f, 1f, 1f);
        boss.SetVelocity(0,0);

    }
}
