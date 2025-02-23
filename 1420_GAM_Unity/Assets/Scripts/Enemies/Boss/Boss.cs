using UnityEditor.Tilemaps;
using UnityEngine;

public class Boss : MonoBehaviour
{
    #region [States]
    public BossStateMachine stateMachine { get; private set; }
    public BossState state;
    public BossDashAttackState dashAttack { get; private set; }
    public BossPlungeState plunge { get; private set; }
    public BossSummonState summon { get; private set; }
    public BossRestState rest { get; private set; }
    #endregion

    #region [Components]
    [Header("Components")]
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Player player;
    [HideInInspector] public int facingDir;
     public int attackPatternCount;
    public Transform rightPoint, leftPoint, topPoint;
    public Transform restPoint;
    #endregion

    #region [Raycasts]
    [Space]
    [Header("Raycasts")]
    public LayerMask ground;
    public Transform groundChecker;
    public float groundDistance;
    public RaycastHit2D isGround;
    #endregion

    #region[Attack and States specs]
    [Space]
    [Header("Attack Specs")]
    public float restDur;
    [HideInInspector] public float restTimer;
    public float dashAttackSpeed;
    public float plungeSpeed;
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
    }

    public void SpawnCrowProjectiles()
    {
        if (crowSpawnersCount == crowSpawners.Length)
            crowSpawnersCount = 0;

        GameObject newCrowProjectile = Instantiate(crowProjectilePrefab, crowSpawners[crowSpawnersCount].position, crowSpawners[crowSpawnersCount].rotation);

        crowSpawnersCount++;
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
    }
}
