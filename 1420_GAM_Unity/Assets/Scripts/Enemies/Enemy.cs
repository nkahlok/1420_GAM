using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine enemyStateMachine { get; private set; }

    private EnemyState enemyState;
    public EnemyIdleState enemyIdleState { get; private set; }
    public EnemyMoveState enemyMoveState { get; private set; }
    public EnemyAggroState enemyAggroState { get; private set;}
    public EnemyAttackState enemyAttackState { get; private set;}

    #region[Enemy Stats]
    [Header("Enemy stats")]
    public float moveSpeed;
    public float idleTime;
    public float facingDir;
    public float aggroDur;
    public float attackCD;
    //public Vector2 knockbackForce;
    [HideInInspector] public float idleCount;
    [HideInInspector] public float aggroCount;
    [HideInInspector] public float attackCount;
    #endregion

    #region[Raycasts]
    [Header("Raycasts")]
    public Transform wallChecker;
    public LayerMask wallLayer;
    public float wallCheckDistance;
    public Transform groundChecker;
    public LayerMask groundLayer;
    public float groundCheckDistance;
    public Transform playerChecker;
    public LayerMask playerLayer;
    public float playerAggroDistance;
    public float playerAttackDistance;
    public float playerAboveDistance;

    [HideInInspector] public RaycastHit2D isWall;
    [HideInInspector] public RaycastHit2D isGround;
    [HideInInspector] public RaycastHit2D isPlayer;
    [HideInInspector] public RaycastHit2D isPlayerAbove;
    #endregion


    [HideInInspector] public Rigidbody2D rb;
    protected Player player;
    protected bool isBusy;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerManager.instance.player;
        enemyStateMachine = new EnemyStateMachine();
        enemyMoveState = new EnemyMoveState(this, enemyStateMachine, "Move");
        enemyIdleState = new EnemyIdleState(this, enemyStateMachine, "Idle");
        enemyAggroState = new EnemyAggroState(this, enemyStateMachine, "Move");
        enemyAttackState = new EnemyAttackState(this, enemyStateMachine, "Attack");
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyStateMachine.Initialize(enemyMoveState);
    
    }

    // Update is called once per frame
    void Update()
    {
        if(isBusy) 
            return;
        else
            enemyStateMachine.currentState.Update();

        CollisionChecks();
    }

    public void Flip()
    {
        this.transform.Rotate(0, 180, 0);
        facingDir *= -1;     
    }

    public void FlipControl(float _xVelocity)
    {
        if (_xVelocity > 0 && facingDir == -1)
        {
            Flip();
        }
        else if (_xVelocity < 0 && facingDir == 1)
        {
            Flip();
        }
    }

    public void SetVelocity(float _x, float _y)
    {
        rb.linearVelocity = new Vector2(_x, _y);
        FlipControl(_x);
    }

    public void CollisionChecks()
    {
        isWall = Physics2D.Raycast(wallChecker.position, Vector2.right*facingDir, wallCheckDistance, wallLayer);
        isGround = Physics2D.Raycast(groundChecker.position, Vector2.down, groundCheckDistance, groundLayer);
        isPlayer = Physics2D.Raycast(playerChecker.position, Vector2.right * facingDir, playerAggroDistance, playerLayer);
        isPlayerAbove = Physics2D.Raycast(playerChecker.position, Vector2.up, playerAboveDistance, playerLayer);    
    }
    public void KnockBack(string attackType)
    {
        if (isBusy)
        {
            return;  
        }
        else
        {
            if(attackType == "Launch Up")
            {
                SetVelocity(SkillManager.instance.launchSkill.launchVelocity[0].x, SkillManager.instance.launchSkill.launchVelocity[0].y);
                StartCoroutine("BusySwitch", 1);
            }
            else if (attackType == "Launch Down")
            {
                SetVelocity(SkillManager.instance.launchSkill.launchVelocity[1].x, SkillManager.instance.launchSkill.launchVelocity[1].y * -1);
                StartCoroutine("BusySwitch", 1);
            }
            else if (attackType == "Forward")
            {
                if (player.transform.position.x < this.transform.position.x)
                {
                    Debug.Log("KnockedBack");
                    SetVelocity(SkillManager.instance.launchSkill.launchVelocity[2].x, SkillManager.instance.launchSkill.launchVelocity[2].y);
                    StartCoroutine("BusySwitch", 1);
                }
                else if (player.transform.position.x > this.transform.position.x)
                {
                    Debug.Log("KnockedBack");
                    SetVelocity(SkillManager.instance.launchSkill.launchVelocity[2].x * -1, SkillManager.instance.launchSkill.launchVelocity[2].y);
                    StartCoroutine("BusySwitch", 1);
                }
            }
            else if (attackType == "Manhole")
            {
               
                SetVelocity(SkillManager.instance.manholeSkill.knockBackForce.x, SkillManager.instance.manholeSkill.knockBackForce.y);
                StartCoroutine("BusySwitch", 1);
            }
            /*else if (player.transform.position.x < this.transform.position.x)
            {
                Debug.Log("KnockedBack");
                SetVelocity(knockbackForce.x, knockbackForce.y);
                StartCoroutine("BusySwitch", 1);
            }
            else if (player.transform.position.x > this.transform.position.x)
            {
                Debug.Log("KnockedBack");
                SetVelocity(knockbackForce.x * -1, knockbackForce.y);
                StartCoroutine("BusySwitch", 1);
            }*/
        }
     
        
    }

    IEnumerator BusySwitch(float seconds)
    {
        isBusy = true; 
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(wallChecker.position.x, wallChecker.position.y), new Vector2(wallChecker.position.x + wallCheckDistance * facingDir, wallChecker.position.y));
        Gizmos.DrawLine(new Vector2(groundChecker.position.x, groundChecker.position.y), new Vector2(groundChecker.position.x, groundChecker.position.y - groundCheckDistance));
        Gizmos.DrawLine(new Vector2(playerChecker.position.x, playerChecker.position.y), new Vector2(playerChecker.position.x + playerAggroDistance * facingDir, playerChecker.position.y));
        Gizmos.DrawLine(new Vector2(playerChecker.position.x, playerChecker.position.y), new Vector2(playerChecker.position.x, playerChecker.position.y + playerAboveDistance));
       
    }

    
}
