using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public PlayerState playerState { get; private set; }
    private IEnumerator defaultGravity;

    #region[Player Components]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public int comboHits = 0;
    [HideInInspector] public int comboCounter = 0;
    [HideInInspector] public bool manholeAvailable;
    public Text comboUI;
    public Text comboNamesUI;
    public int facingDir = 1;
    public bool isBusy;
    public bool caneWpn;
    public bool doubleJumpEnabled;
    public GameObject caneEquipped;
    public GameObject manholeEquipped;
    public ParticleSystem jumpFeathers;
    public ParticleSystem hitEffect;
    #endregion

    [Space]
    public string[] comboNames;

    [Space]
    #region [Graphics]
    [Header("Graphics")]
    public GameObject manholeIndicator;
    #endregion

    #region[States]
    public P_IdleState idle { get; private set; }
    public P_MoveState move { get; private set; }
    public P_JumpState jump { get; private set; }
    public P_FallState fall { get; private set; }
    public P_DashState dash { get; private set; }
    public P_AttackState attack { get; private set; }
    public P_ManHoleAimingState manHoleAim {  get; private set; }
    public P_LaunchAttackState launchAttack { get; private set; }   
    public P_AerialAttackState aerialAttack { get; private set; }

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
    [Space]
    public Transform manholeThrowChecker;
    public float manHoleThrowRange;

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
    [HideInInspector] public float comboHitCount;
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
        aerialAttack = new P_AerialAttackState(this, stateMachine, "Attack");
        manHoleAim = new P_ManHoleAimingState(this, stateMachine, "Aiming");
        launchAttack = new P_LaunchAttackState(this, stateMachine, "Launch");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        defaultGravity = DefaultGravity(0.2f);

    }


    void Start()
    {
     
        stateMachine.Initialize(idle);
        caneWpn = true;
        manholeAvailable = true;
        caneEquipped.SetActive(false);
        manholeEquipped.SetActive(false);
 
       
        
    }

    
    void Update()
    {
        stateMachine.currentState.Update();
        CollisionChecks();
        UseSkill();
        WeaponSwap();
        ComboCounterUI();      
     
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

    public void FlipControl(float _x)
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
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isBusy && isGround && !caneWpn)
        {
            
            SkillManager.instance.manholeSkill.SkillAvailable();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            SkillManager.instance.dashSkill.SkillAvailable();
            
        }

        if((Input.GetKeyUp(KeyCode.Mouse1) && !isBusy && caneWpn && isGround)|| (Input.GetKeyUp(KeyCode.Mouse1) && !isBusy && caneWpn && SkillManager.instance.launchSkill.launchDown))
        {
            SkillManager.instance.launchSkill.SkillAvailable();
        }
    }

    private void WeaponSwap()
    {

        if (Input.mouseScrollDelta.y != 0 && !isBusy && manholeAvailable)
        {
            caneWpn = !caneWpn;
        }

        if (caneWpn)
        {
            caneEquipped.SetActive(true);
            manholeEquipped.SetActive(false);
        }
        else
        {
            caneEquipped.SetActive(false);
            manholeEquipped.SetActive(true);
        }
    }

    private void ComboCounterUI()
    {
        if (comboHitCount < 0)
            comboHits = 0;

        switch (comboHits) 
        {
            case int x when x>= 8 && x<12:
                comboNamesUI.gameObject.SetActive(true);
                comboNamesUI.text = comboNames[0];
                break;
            case int x when x >= 12 && x < 16:
                comboNamesUI.gameObject.SetActive(true);
                comboNamesUI.text = comboNames[1];
                break;
            case int x when x >= 16 && x < 20:
                comboNamesUI.gameObject.SetActive(true);
                comboNamesUI.text = comboNames[2];
                break;
            case >=20:
                comboNamesUI.gameObject.SetActive(true);
                comboNamesUI.text = comboNames[3];
                break;
                default:
                comboNamesUI.gameObject.SetActive(false);   
                break;

        }

        if(comboHits == 0)
            comboUI.gameObject.SetActive(false);
        else 
            comboUI.gameObject.SetActive(true);

        comboUI.text = $"Combo {comboHits / 2}";

        comboHitCount -= Time.deltaTime;
    }


    public void GravityCoroutine()
    {
        StopCoroutine(defaultGravity);
        defaultGravity = DefaultGravity(0.2f);
        StartCoroutine(defaultGravity);
    }

    private IEnumerator DefaultGravity(float seconds)
    {
        rb.gravityScale = 0;
        yield return new WaitForSeconds(seconds);
        rb.gravityScale = 3;
        isBusy = false; 
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ManHolePhysics>() != null)
        {
            //gameObject.transform.SetParent(collision.gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ManHolePhysics>() != null)
        {
            //gameObject.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(groundChecker.position.x, groundChecker.position.y), new Vector2(groundChecker.position.x, groundChecker.position.y - groundDistance));
        Gizmos.DrawLine(new Vector2(wallChecker.position.x, wallChecker.position.y), new Vector2(wallChecker.position.x + wallDistance * facingDir, wallChecker.position.y));
        Gizmos.DrawWireSphere(new Vector2(meleeAttackChecker.position.x, meleeAttackChecker.position.y), meleeAttackRange);
        //Gizmos.DrawLine(new Vector2(manholeThrowChecker.position.x, manholeThrowChecker.position.y), new Vector2(manholeThrowChecker.position.x + manHoleThrowRange * manHoleAim.xDir, manholeThrowChecker.position.y + manHoleThrowRange*manHoleAim.yDir));
    }
}
