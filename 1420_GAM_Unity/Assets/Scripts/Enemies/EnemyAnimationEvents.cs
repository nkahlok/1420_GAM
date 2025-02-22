using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    Player player;
    Enemy enemy;

    void Start()
    {
        player = PlayerManager.instance.player;
        enemy = GetComponentInParent<Enemy>();
    }

    private void Attack()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.meleeAttackChecker.position, enemy.meleeAttackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Player>() != null)
            {
                player.Damage(enemy.catDamage);

                if (this.gameObject.transform.position.x > player.transform.position.x && player.facingDir == -1)
                {
                    player.KnockBack(-1 * enemy.normalAttackKnockBack.x, enemy.normalAttackKnockBack.y);
                }
                else if (this.gameObject.transform.position.x < player.transform.position.x && player.facingDir == 1)
                {
                    player.KnockBack(-1 * enemy.normalAttackKnockBack.x, enemy.normalAttackKnockBack.y);
                }
                else
                {
                    player.KnockBack(enemy.normalAttackKnockBack.x, enemy.normalAttackKnockBack.y);
                }

            }

        }
    }

    private void EndAttack()
    {
        enemy.enemyStateMachine.Changestate(enemy.enemyAggroState);
    }

    private void CounterWindowOn() => enemy.CounterWindowOn();

    private void CounterWindowOff() => enemy.CounterWindowOff();

}
