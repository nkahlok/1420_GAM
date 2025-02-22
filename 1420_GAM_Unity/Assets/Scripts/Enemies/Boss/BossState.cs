using UnityEngine;

public class BossState 
{
    Boss boss;
    BossStateMachine stateMachine;
    string animBool;

    protected Animator anim;
    protected Rigidbody2D rb;

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
        anim.SetBool(animBool, true);
    }


    public virtual void Exit()
    {
        anim.SetBool(animBool, false);
    }

    public virtual void Update()
    {

    }
}
