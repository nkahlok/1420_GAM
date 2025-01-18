using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class EnemyState
{
    public Enemy enemy;
    public EnemyStateMachine enemyStateMachine;
    public string animBool;
    public Rigidbody2D rb;
    public Player player;

    public EnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBool)
    {
        this.enemy = _enemy;
        this.enemyStateMachine = _enemyStateMachine;
        this.animBool = _animBool;

    }

    public virtual void Enter()
    {
        rb = enemy.rb;
        player = PlayerManager.instance.player;
    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {
        enemy.idleCount -= Time.deltaTime;
    }
}
