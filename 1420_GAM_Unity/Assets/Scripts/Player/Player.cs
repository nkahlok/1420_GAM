using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    private PlayerState playerState;

    #region[Player Components]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public int facingDir = 1;
    #endregion

    #region[States]
    public P_IdleState idle { get; private set; }
    public P_MoveState move { get; private set; }
    public P_JumpState jump { get; private set; }
    public P_FallState fall { get; private set; }
    public P_DashState dash { get; private set; }
    public P_AttackState attack { get; private set; }

    #endregion


    #region[Vectors]
    [Header("Forces")]
    public float moveSpeed;
    public float jumpForce;
    public Vector2[] attackMovement;

    #endregion

    #region[Raycasts]
    [Header("Raycasts")]  
    public LayerMask ground;
    public Transform groundChecker;
    public float groundDistance;
    public RaycastHit2D isGround;
    [Space]
    public LayerMask wall;
    public Transform wallChecker;
    public float wallDistance;
    public RaycastHit2D isWall;
    [Space]
    public Transform meleeAttackChecker;
    public float meleeAttackRange;
    #endregion
    [Space]

    #region[Timers]
    [Header("Timers")]
    public float jumpBufferTime;
    [HideInInspector] public float jumpBufferCount;
    public float coyoteTime;
    [HideInInspector] public float coyoteCount;
    [HideInInspector] public bool coyoteEnabled = false;
    public float comboTime;
    [HideInInspector] public float comboCount;
    #endregion

    void Awake()
    {
        stateMachine = new StateMachine();
        idle = new P_IdleState(this, stateMachine, "Idle");
        move = new P_MoveState(this, stateMachine, "Move");
        jump = new P_JumpState(this, stateMachine, "Jump");
        fall = new P_FallState(this, stateMachine, "Jump");
        dash = new P_DashState(this, stateMachine, "Dash");
        attack = new P_AttackState(this, stateMachine, "Attack");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();    
    }


    void Start()
    {
     
        stateMachine.Initialize(idle);
        
    }

    
    void Update()
    {
        stateMachine.currentState.Update();
        CollisionChecks();
        UseSkill();
    }

    public void CollisionChecks()
    {
        isGround = Physics2D.Raycast(groundChecker.position, Vector2.down, groundDistance, ground);
        isWall = Physics2D.Raycast(wallChecker.position, Vector2.right * facingDir, wallDistance, wall);
    }

    public void SetVelocity(float _xVelo, float _yVelo)
    {
        rb.linearVelocity = new Vector2(_xVelo, _yVelo);
        FlipControl(_xVelo);
    }

    private void FlipControl(float _x)
    {
        if(_x > 0 && facingDir == -1)
        {
            this.transform.Rotate(0, 180, 0);
            facingDir *= -1;
        }

        if(_x < 0 && facingDir == 1)
        {
            this.transform.Rotate(0, 180, 0);
            facingDir *= -1;
        }

    }

    private void UseSkill()
    {
        if (Input.GetKey(KeyCode.E))
        {
            SkillManager.instance.skill1.SkillAvailable();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            SkillManager.instance.dashSkill.SkillAvailable();
            
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(groundChecker.position.x, groundChecker.position.y), new Vector2(groundChecker.position.x, groundChecker.position.y - groundDistance));
        Gizmos.DrawLine(new Vector2(wallChecker.position.x, wallChecker.position.y), new Vector2(wallChecker.position.x + wallDistance * facingDir, wallChecker.position.y));
        Gizmos.DrawWireSphere(new Vector2(meleeAttackChecker.position.x, meleeAttackChecker.position.y), meleeAttackRange);
    }

}