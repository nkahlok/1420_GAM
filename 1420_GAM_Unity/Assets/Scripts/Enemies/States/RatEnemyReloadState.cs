using UnityEngine;

public class RatEnemyReloadState : EnemyState
{
    protected float stateDur;
    public RatEnemyReloadState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBool) : base(_enemy, _enemyStateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateDur = enemy.reloadTime;
        SoundManager.PlaySfx(SoundType.RATRELOAD);
        enemy.aggroImg.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.aggroImg.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        stateDur -= Time.deltaTime; 

        if(stateDur < 0)
        {
            enemy.enemyStateMachine.Changestate(enemy.enemyAggroState);
        }
    }
}
