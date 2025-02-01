using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class EnemyState
{
    public Enemy enemy;
    public Animator anim;
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
        anim = enemy.anim;
        anim.SetBool(animBool, true);
    }

    public virtual void Exit()
    {
        anim.SetBool(animBool, false);
    }

    public virtual void Update()
    {
        enemy.idleCount -= Time.deltaTime;
        enemy.aggroCount -= Time.deltaTime;   
        enemy.airborneCount -= Time.deltaTime;  
        anim.SetFloat("xVelocity", rb.linearVelocityX);
        anim.SetFloat("yVelocity", rb.linearVelocityY);
    }
}
