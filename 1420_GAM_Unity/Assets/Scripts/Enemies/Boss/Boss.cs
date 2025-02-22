using UnityEngine;

public class Boss : MonoBehaviour
{
    #region [States]
    public BossStateMachine stateMachine { get; private set; }
    public BossState state;
    public BossDashAttackState dashAttack { get; private set; }
    #endregion

    #region [Components]
    [Header("Components")]
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Player player;
    #endregion

    #region [Raycasts]
    [Space]
    [Header("Raycasts")]
    public LayerMask ground;
    public Transform groundChecker;
    public float groundDistance;
    public RaycastHit2D isGround;
    #endregion

    private void Awake()
    {
        stateMachine = new BossStateMachine();
        dashAttack = new BossDashAttackState(this, stateMachine, "Dash");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerManager.instance.player;
        stateMachine.Initialize(dashAttack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(groundChecker.position.x, groundChecker.position.y), new Vector2(groundChecker.position.x, groundChecker.position.y - groundDistance));
    }
}
