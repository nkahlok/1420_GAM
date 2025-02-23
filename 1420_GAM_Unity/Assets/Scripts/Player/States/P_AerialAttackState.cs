using UnityEngine;

public class P_AerialAttackState : P_AttackState
{
    public P_AerialAttackState(Player _player, StateMachine _stateMachine, string _animBool) : base(_player, _stateMachine, _animBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.airBorneCount = player.airBorneTime;
    
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

                enemy.airborneCount = enemy.airborneTime;
                enemy.KnockBack("Aerial");

            }
            else if(collider.GetComponent<Boss>() != null)
            {
                Boss boss = collider.GetComponent<Boss>();
                boss.KnockBack("Aerial");
            }
        }

        rb.gravityScale = 0;
    }
}
