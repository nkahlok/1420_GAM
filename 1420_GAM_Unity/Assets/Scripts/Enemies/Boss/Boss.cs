using System.Collections;
//using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Boss : MonoBehaviour
{
    #region [States]
    public BossStateMachine stateMachine { get; private set; }
    public BossState state;
    public BossDashAttackState dashAttack { get; private set; }
    public BossPlungeState plunge { get; private set; }
    public BossSummonState summon { get; private set; }
    public BossKnockedState knocked { get; private set; }   
    public BossTiredState tired { get; private set; }   
    public BossRestState rest { get; private set; }
    #endregion

    #region [Components]
    [Header("Components")]
    [HideInInspector] public EnemyTypeModifier modifier;
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Player player;
    [HideInInspector] public bool isBusy;
    [HideInInspector] public int facingDir;
    [HideInInspector] public bool canBeCountered;
    [HideInInspector] public bool countered;
    [HideInInspector] public bool knockedDown;
    [HideInInspector] public int attackPatternCount;
    [HideInInspector] public bool isTired;
    [HideInInspector] public bool tiredTriggered;
    [HideInInspector] public bool bossDeathSfxCalled = false;
    private bool waitingForHitStop;
    public Transform rightPoint, leftPoint, topPoint;
    public Transform restPoint, tiredPoint;
    #endregion

    #region [VFX]
    [Space]
    [Header("VFX")]
    public ShockwaveManager shockwaveManager;
    public GameObject counterWindow;
    public ParticleSystem dashDust;
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

    #region [AttackPattern]
    [Space]
    [Header("Attack Patterns - Phase One(Maximum 4 numbers: 0 to 3)")]
    [HideInInspector] public bool phaseOne;
    public int[] dashAttackPatternNumberOne;
    public int[] plungeAttackPatternNumberOne;
    public int[] summonAttackPatternNumberOne;
    [Header("Attack Patterns - Phase Two(Maximum 4 numbers: 0 to 3)")]
    [HideInInspector] public bool phaseTwo;
    public int[] dashAttackPatternNumberTwo;
    public int[] plungeAttackPatternNumberTwo;
    public int[] summonAttackPatternNumberTwo;
    [Header("Attack Patterns - Phase Three(Maximum 4 numbers: 0 to 3)")]
    [HideInInspector] public bool phaseThree;
    public int[] dashAttackPatternNumberThree;
    public int[] plungeAttackPatternNumberThree;
    public int[] summonAttackPatternNumberThree;
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
    public float knockedDownDur;
    public int[] tiredHpThresholds;
    #endregion

    #region[Phase Two Attack and States specs]
    [Space]
    [Header("Phase Two and States specs")]
    public GameObject[] shockwaveProjectilePrefab;
    public Transform shockwaveProjectileSpawner;
    #endregion

    #region[Phase Three Attack and States specs]
    [Space]
    [Header("Phase Two and States specs")]
    public GameObject birdwallPrefab;
    public Transform birdwallSpawner;
    #endregion

    public LevelLoaderScript levelLoader;
    private void Awake()
    {
        stateMachine = new BossStateMachine();
        dashAttack = new BossDashAttackState(this, stateMachine, "Dash");
        rest = new BossRestState(this, stateMachine, "Idle");
        plunge = new BossPlungeState(this, stateMachine, "Plunge");
        summon = new BossSummonState(this, stateMachine, "Summon");
        knocked = new BossKnockedState(this, stateMachine, "Knocked");
        tired = new BossTiredState(this, stateMachine, "Tired");

        modifier = GetComponent<EnemyTypeModifier>();
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
        counterWindow.SetActive(false);
        modifier.canBeDamaged = false;
        phaseOne = true;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();

        CollisionChecks();
        PhaseOneTired();
        PhaseTwoTired();

        if (attackPatternCount > 3 )
        {
            attackPatternCount = 0;
        }

        if (knockedDown)
        {
            stateMachine.ChangeState(knocked);
        }

        if (modifier.hits == 0)
        {
            Time.timeScale = 1;
            StartCoroutine(BossDeathCoroutine());
        }

        //topPoint.position = new Vector2(player.transform.position.x, topPoint.position.y);
     
    }

    private IEnumerator BossDeathCoroutine() 
    {
        if (!bossDeathSfxCalled)
        {
            bossDeathSfxCalled = true;
            SoundManager.PlaySfx(SoundType.BOSSDEATH);
            //boss death anim goes here also...
            yield return new WaitForSeconds(5f);
            levelLoader.LoadNextLevel();
        }
    }

    public void PhaseOneTired()
    {
        if (modifier.hits == tiredHpThresholds[0] && !tiredTriggered)
        {
            stateMachine.ChangeState(tired);
            modifier.A.SetActive(true);
        }
        //when phase one ends
        if (modifier.hits == tiredHpThresholds[0] - player.comboNum[3] && phaseOne == true)
        {
            tiredTriggered = false;
            isTired = false;
            phaseOne = false;
            phaseTwo = true;
            attackPatternCount = 0;
            modifier.A.SetActive(false);
            stateMachine.ChangeState(rest);
        }
    }

    public void PhaseTwoTired()
    {
        if(modifier.hits == tiredHpThresholds[1] && !tiredTriggered)
        {
            stateMachine.ChangeState(tired);
            modifier.A.SetActive(true);
        }

        if(modifier.hits == tiredHpThresholds[1] - player.comboNum[3] && phaseTwo == true)
        {
            tiredTriggered = false;
            isTired = false;
            phaseTwo = false;
            phaseThree = true;  
            attackPatternCount = 0;
            modifier.A.SetActive(false);
            stateMachine.ChangeState(rest);
        }


    }

    public void SpawnBirdWall()
    {
        SoundManager.PlaySfx(SoundType.BOSSMULTPROJATK);
        GameObject newBirdWall = Instantiate(birdwallPrefab, birdwallSpawner.position, birdwallSpawner.rotation);
    }

    public void SpawnCrowProjectiles()
    {
        if (crowSpawnersCount == crowSpawners.Length)
            crowSpawnersCount = 0;

        crowSpawners[crowSpawnersCount].gameObject.GetComponent<SpriteRenderer>().enabled = true;
        SoundManager.PlaySfx(SoundType.BOSSPROJATK);
        GameObject newCrowProjectile = Instantiate(crowProjectilePrefab, crowSpawners[crowSpawnersCount].position, crowSpawners[crowSpawnersCount].rotation);

        crowSpawnersCount++;
    }

    public void SpawnShockwave()
    {
        SoundManager.PlaySfx(SoundType.BOSSFALLATKIMPACT);
        GameObject newShockwaveProjectile = Instantiate(shockwaveProjectilePrefab[0], shockwaveProjectileSpawner.position, shockwaveProjectileSpawner.rotation);
        GameObject newShockwaveProjectile2 = Instantiate(shockwaveProjectilePrefab[1], shockwaveProjectileSpawner.position, shockwaveProjectileSpawner.rotation);
    }

    public void KnockBack(string attackType)
    {
        if (isBusy)
            return;

        else if (attackType == "Launch Up" && isTired)
        {
            rb.linearVelocity = new Vector2(SkillManager.instance.launchSkill.launchVelocity[0].x, SkillManager.instance.launchSkill.launchVelocity[0].y);
            StartCoroutine("BusySwitch", 0);
        }
        else if (attackType == "Countered")
        {
            Debug.Log("Countered");
            SoundManager.PlaySfx(SoundType.PLAYERPARRYSUCCESS);
            countered = true;
            knockedDown = true;
            shockwaveManager.CallShockwave();
            StartCoroutine("BusySwitch", 0);
            player.mainCam.GetComponentInChildren<Animator>().SetTrigger("Shake");
            HitStop(player.counterHitStop);

        }
        //this is the aerrial bounce code
        else if (attackType == "Aerial" && isTired)
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

    IEnumerator BusySwitch(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
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
        if (collision.gameObject.CompareTag("ArenaBounds") && !isTired)
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
