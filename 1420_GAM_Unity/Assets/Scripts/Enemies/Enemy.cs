using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public bool wasAttacked;
    [HideInInspector] public bool knockedDown;
    [HideInInspector] public bool launchDown;
    [HideInInspector] public bool canBeCountered;
    [HideInInspector] public bool countered;

    #region[Enemy States]
    public EnemyStateMachine enemyStateMachine { get; private set; }

    private EnemyState enemyState;
    public EnemyIdleState enemyIdleState { get; private set; }
    public EnemyMoveState enemyMoveState { get; private set; }
    public EnemyAggroState enemyAggroState { get; private set;}
    public EnemyAttackState enemyAttackState { get; private set;}
    public EnemyKnockDownState enemyKnockDownState { get; set; }
    public RatEnemyReloadState ratEnemyReloadState { get; set; }    
    #endregion

    #region[Enemy type]
    public bool isCat;
    public bool isRat;
    #endregion

    #region[Enemy Stats]
    [Header("Enemy stats")]
    public float moveSpeed;
    public float idleTime;
    public float facingDir;
    public float aggroDur;
    public float attackCD;
    public float airborneTime;
    //public Vector2 knockbackForce;
    [HideInInspector] public float idleCount;
    [HideInInspector] public float aggroCount;
    [HideInInspector] public float attackCount;
    [HideInInspector] public float airborneCount;
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

    [Space]
    #region[Graphics & VFX]
    [Header("Graphics")]
    public GameObject aggroImg;
    public GameObject counterWindowImg;
    #endregion


    #region Rat
    [Header("Rat specific")]
    [Space]
    [Header("Rat stats")]
    public float firingDur;
    public float reloadTime;
    #region[Bullets]
    [Header("Bullets")]
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform bulletSpawner;
    #endregion
    #endregion

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Player player;
    [HideInInspector] public bool isBusy;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();       
        enemyStateMachine = new EnemyStateMachine();
        enemyMoveState = new EnemyMoveState(this, enemyStateMachine, "Move");
        enemyIdleState = new EnemyIdleState(this, enemyStateMachine, "Idle");
        enemyAggroState = new EnemyAggroState(this, enemyStateMachine, "Aggro");
        enemyAttackState = new EnemyAttackState(this, enemyStateMachine, "Attack");
        enemyKnockDownState = new EnemyKnockDownState(this, enemyStateMachine, "Knocked");
        ratEnemyReloadState = new RatEnemyReloadState(this, enemyStateMachine, "Reload");
    }

    // Start is called before the first frame update
    void Start()
    {
       
        player = PlayerManager.instance.player;
        enemyStateMachine.Initialize(enemyMoveState);
        knockedDown = false;
        launchDown = false;
        canBeCountered = false;
        countered = false;
        aggroImg.SetActive(false); 
        counterWindowImg.SetActive(false);  
    
    }

    // Update is called once per frame
    void Update()
    {
        if(isBusy) 
            return;
        else
            enemyStateMachine.currentState.Update();

    
        if(knockedDown)//only up launch attacks activate this bool
            enemyStateMachine.Changestate(enemyKnockDownState);

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

    public void SpawnBullets()
    {
        GameObject newBulletPrefab = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);

        newBulletPrefab.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(bulletSpeed * facingDir, 0);

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
                rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[0].x, SkillManager.instance.launchSkill.launchVelocity[0].y);
                StartCoroutine("BusySwitch", 0);
            }
            else if (attackType == "Launch Down")
            {
                Debug.Log("Down");
                airborneCount = -1;
                launchDown = true;
                //move this velocity code over to knockdown state if using anti gravity
                rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[1].x, SkillManager.instance.launchSkill.launchVelocity[1].y * -1);
                StartCoroutine("BusySwitch", 0);
            }
            else if (attackType == "Forward")
            {
                if (player.transform.position.x < this.transform.position.x)
                {
                    //Debug.Log("KnockedBack");
                    rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[2].x, SkillManager.instance.launchSkill.launchVelocity[2].y);
                    StartCoroutine("BusySwitch", 1);
                }
                else if (player.transform.position.x > this.transform.position.x)
                {
                    //Debug.Log("KnockedBack");
                    rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[2].x * -1, SkillManager.instance.launchSkill.launchVelocity[2].y);
                    StartCoroutine("BusySwitch", 1);
                }
            }
            else if (attackType == "Countered")
            {
                Debug.Log("Countered");
                //velocity code moved to knock down state
                //rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[3].x * facingDir * -1, SkillManager.instance.launchSkill.launchVelocity[3].y);
                countered = true;
                knockedDown = true;
                StartCoroutine("BusySwitch", 0);
            }
            else if (attackType == "Manhole")
            {
                rb.linearVelocity = new Vector2(SkillManager.instance.manholeSkill.knockBackForce.x, SkillManager.instance.manholeSkill.knockBackForce.y);
                StartCoroutine("BusySwitch", 0.2);
            }
            else if(attackType == "Normal")
            {
                if (player.transform.position.x < this.transform.position.x)
                {
                    rb.linearVelocity = new Vector2(player.attackMovement[0].x, 3);
                    //SetVelocity(0.5f, 5);
                    StartCoroutine("BusySwitch", 0.2);
                }
                else if (player.transform.position.x > this.transform.position.x)
                {
                    rb.linearVelocity = new Vector2(player.attackMovement[0].x * -1, 3);
                    //SetVelocity(0.5f * -1, 5);
                    StartCoroutine("BusySwitch", 0.2);
                }
               
            }//this is the aerrial bounce code
            else if(attackType == "Aerial")
            {
                rb.linearVelocity = new Vector2(0, player.aerialBounceForce);

            }
       
        }
     
        
    }
    public void CounterWindowOn()
    {
        canBeCountered = true;
        counterWindowImg.SetActive(true);
    }
    public void CounterWindowOff()
    {
        canBeCountered = false;
        counterWindowImg.SetActive(false);
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

