using UnityEngine;

public class P_AerialAttackState : P_AttackState
{
    public P_AerialAttackState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
        player.GravityCoroutine();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = collider.GetComponent<Enemy>();

                enemy.AirlockCoroutine();

            }
        }
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, 0);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackChecker.position, player.meleeAttackRange);

        
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.airlock = true;
                //enemy.enemyStateMachine.Changestate(enemy.enemyKnockDownState);

            }
        }

        rb.gravityScale = 0;
    }
}
