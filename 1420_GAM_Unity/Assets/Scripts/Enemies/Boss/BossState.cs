using UnityEngine;

public class BossState 
{
    protected Boss boss;
    protected BossStateMachine stateMachine;
    protected string animBool;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;
    protected Color tempColor;

    

    public BossState(Boss _boss, BossStateMachine _stateMachine, string _animBool)
    {
        this.boss = _boss;
        this.stateMachine = _stateMachine;
        this.animBool = _animBool;
    }

    public virtual void Enter()
    {
        anim = boss.anim;
        rb = boss.rb;
        sprite = boss.sprite;
        tempColor = sprite.color;
        anim.SetBool(animBool, true);
    }


    public virtual void Exit()
    {
        anim.SetBool(animBool, false);
    }

    public virtual void Update()
    {
        anim.SetFloat("yVelocity", rb.linearVelocityY); 
    }
}
