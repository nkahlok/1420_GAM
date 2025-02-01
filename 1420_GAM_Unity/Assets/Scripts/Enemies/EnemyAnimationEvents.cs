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

    private void EndAttack()
    {
        enemy.enemyStateMachine.Changestate(enemy.enemyAggroState);
    }


}
