using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine enemyStateMachine { get; private set; }

    private EnemyState enemyState;
    public EnemyIdleState enemyIdleState { get; private set; }
    public EnemyMoveState enemyMoveState { get; private set; }

    public Transform wallChecker;
    public LayerMask wallLayer;
    public float wallCheckDistance;
    public Transform groundChecker;
    public LayerMask groundLayer;
    public float groundCheckDistance;

    [HideInInspector] public RaycastHit2D isWall;
    [HideInInspector] public RaycastHit2D isGround;

    public float facingDir;

    [HideInInspector] public Rigidbody2D rb;

    public float moveSpeed;
    public float idleTime;
    [HideInInspector] public float idleCount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyStateMachine = new EnemyStateMachine();
        enemyMoveState = new EnemyMoveState(this, enemyStateMachine, "Move");
        enemyIdleState = new EnemyIdleState(this, enemyStateMachine, "Idle");
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyStateMachine.Initialize(enemyMoveState);
    
    }

    // Update is called once per frame
    void Update()
    {
        enemyStateMachine.currentState.Update();
        CollisionChecks();
    }

    public void Flip()
    {
        this.transform.Rotate(0, 180, 0);
        facingDir *= -1;
    }

    public void SetVelocity(float _x, float _y)
    {
        rb.linearVelocity = new Vector2(_x, _y);
    }

    public void CollisionChecks()
    {
        isWall = Physics2D.Raycast(wallChecker.position, Vector2.right*facingDir, wallCheckDistance, wallLayer);
        isGround = Physics2D.Raycast(groundChecker.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(wallChecker.position.x, wallChecker.position.y), new Vector2(wallChecker.position.x + wallCheckDistance * facingDir, wallChecker.position.y));
        Gizmos.DrawLine(new Vector2(groundChecker.position.x, groundChecker.position.y), new Vector2(groundChecker.position.x, groundChecker.position.y - groundCheckDistance));
    }
    
}
