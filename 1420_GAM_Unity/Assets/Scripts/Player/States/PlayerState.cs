using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public Player player;
    public StateMachine stateMachine;
    public string animBool;

    public float xInput;
    public float yInput;
    public int comboCounter;


    protected Rigidbody2D rb;    
    protected Animator anim;

    

    public PlayerState(Player _player, StateMachine _stateMachine, string _animBool)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBool = _animBool; 
    }

    virtual public void Enter()
    {
        anim = player.anim;
        rb = player.rb;
        anim.SetBool(animBool, true);

    }

    virtual public void Exit()
    {
        anim.SetBool(animBool, false);
    }

    virtual public void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        player.jumpBufferCount -= Time.deltaTime;
        player.coyoteCount -= Time.deltaTime;
        player.comboCount -= Time.deltaTime;    


        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }


}
