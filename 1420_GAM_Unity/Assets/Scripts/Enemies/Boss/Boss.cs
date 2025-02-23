using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Boss : MonoBehaviour
{
    #region [States]
    public BossStateMachine stateMachine { get; private set; }
    public BossState state;
    public BossDashAttackState dashAttack { get; private set; }
    public BossPlungeState plunge { get; private set; }
    public BossSummonState summon { get; private set; }
    public BossKnockedState knocked { get; private set; }   
    public BossRestState rest { get; private set; }
    #endregion

    #region [Components]
    [Header("Components")]
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Player player;
    [HideInInspector] public int facingDir;
    [HideInInspector] public bool canBeCountered;
    [HideInInspector] public bool countered;
    [HideInInspector] public bool knockedDown;
    private bool waitingForHitStop;
    public int attackPatternCount;
    public Transform rightPoint, leftPoint, topPoint;
    public Transform restPoint;
    #endregion

    #region [VFX]
    [Space]
    [Header("VFX")]
    public ShockwaveManager shockwaveManager;
    public GameObject counterWindow;
    #endregion

    #region [Raycasts]
    [Space]
    [Header("Raycasts")]
    public LayerMask ground;
    public Transform groundChecker;
    public float groundDistance;
    public RaycastHit2D isGround;
    public Transform dashAttackChecker;
    public float dashAttackRange;
    public Transform plungeAttackChecker;
    public float plungeAttackRange;
    #endregion

    #region[Attack and States specs]
    [Space]
    [Header("Attack Specs")]
    public float restDur;
    [HideInInspector] public float restTimer;
    public float dashAttackSpeed;
    public int dashAttackDamage;
    public float plungeSpeed;
    public int plungeAttackDamage;
    public float summonDur;
    [HideInInspector] public float summonTimer;
    public GameObject crowProjectilePrefab;
    public float crowProjectileIntervals;
    public Transform[] crowSpawners;
    [HideInInspector] public int crowSpawnersCount;
    #endregion

    private void Awake()
    {
        stateMachine = new BossStateMachine();
        dashAttack = new BossDashAttackState(this, stateMachine, "Dash");
        rest = new BossRestState(this, stateMachine, "Idle");
        plunge = new BossPlungeState(this, stateMachine, "Plunge");
        summon = new BossSummonState(this, stateMachine, "Summon");
        knocked = new BossKnockedState(this, stateMachine, "Knocked");

        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        facingDir = 1;
        player = PlayerManager.instance.player;
        stateMachine.Initialize(dashAttack);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();

        CollisionChecks();

        if(attackPatternCount > 3 )
        {
            attackPatternCount = 0;
        }

        if (knockedDown)
        {
            stateMachine.ChangeState(knocked);
        }
    }

    public void SpawnCrowProjectiles()
    {
        if (crowSpawnersCount == crowSpawners.Length)
            crowSpawnersCount = 0;

        GameObject newCrowProjectile = Instantiate(crowProjectilePrefab, crowSpawners[crowSpawnersCount].position, crowSpawners[crowSpawnersCount].rotation);

        crowSpawnersCount++;
    }

    public void KnockBack(string attackType)
    {

            if (attackType == "Launch Up")
            {
                rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[0].x, SkillManager.instance.launchSkill.launchVelocity[0].y);
                StartCoroutine("BusySwitch", 0);
            }
            else if (attackType == "Countered")
            {
                Debug.Log("Countered");
            countered = true;
                knockedDown = true;
                shockwaveManager.CallShockwave();
                StartCoroutine("BusySwitch", 0);
                player.mainCam.GetComponentInChildren<Animator>().SetTrigger("Shake");
                HitStop(player.counterHitStop);

            }
            //this is the aerrial bounce code
            else if (attackType == "Aerial")
            {
                rb.linearVelocity = new Vector2(0, player.aerialBounceForce);
            }
    }

    public void HitStop(float duration)
    {
        if (waitingForHitStop)
            return;

        Time.timeScale = 0.0f;
        StartCoroutine(HitStopCorountine(duration));
    }

    IEnumerator HitStopCorountine(float seconds)
    {
        if (!waitingForHitStop)
        {
            waitingForHitStop = true;
            yield return new WaitForSecondsRealtime(seconds);
            Time.timeScale = 1f;
            waitingForHitStop = false;
        }

    }


    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2 (xVelocity, yVelocity);
        FlipController(xVelocity);

    }

    private void FlipController(float x)
    {
        if (facingDir == 1 && x < 0)
        {
            Flip();
        }
        else if (facingDir == -1 && x > 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        this.transform.Rotate(0, 180, 0);
        facingDir *= -1;
    }

    public void CollisionChecks()
    {
        isGround = Physics2D.Raycast(groundChecker.position, Vector2.down, groundDistance, ground);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ArenaBounds"))
        {
            Flip();
            stateMachine.ChangeState(rest);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(groundChecker.position.x, groundChecker.position.y), new Vector2(groundChecker.position.x, groundChecker.position.y - groundDistance));
        Gizmos.DrawWireSphere(new Vector3(dashAttackChecker.position.x, dashAttackChecker.position.y), dashAttackRange);
        Gizmos.DrawWireSphere(new Vector3(plungeAttackChecker.position.x, plungeAttackChecker.position.y), plungeAttackRange);
    }
}
