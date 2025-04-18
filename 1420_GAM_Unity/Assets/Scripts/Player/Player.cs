using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //for alpha use onli
    [HideInInspector] public bool timeStopOnce;
    [HideInInspector] public GameObject counterAttackTutCanvas;
    //ends here

    public StateMachine stateMachine { get; private set; }
    public PlayerState playerState { get; private set; }
    public ShockwaveManager shockwaveManager;
    public RavenousVfxManager ravenousVfxManager;
    public PlayerDeathVfxScript playerDeathVfx;
    private IEnumerator defaultGravity;

    #region[Player Components]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public int comboHits = 0;
    [HideInInspector] public int comboCounter = 0;
    [HideInInspector] public bool manholeAvailable;   
    [HideInInspector] public int facingDir = 1;
    [HideInInspector] public bool isBusy;
    [HideInInspector] public bool caneWpn;
    [HideInInspector] public bool wpnSwapSFXCalled; //weapon swap sfx
    [HideInInspector] public int comboUpSFXCalledD = 0; //combo up sfx
    [HideInInspector] public int comboUpSFXCalledC = 0; //combo up sfx
    [HideInInspector] public int comboUpSFXCalledB = 0; //combo up sfx
    [HideInInspector] public int comboUpSFXCalledR = 0; //combo up sfx
    [HideInInspector] public bool doubleJumpEnabled;
    [HideInInspector] public bool isShield;
    [HideInInspector] public bool cannotBeKnocked;
    [HideInInspector] public bool canBeDamaged;
    [HideInInspector] public bool feathersOn;
    [HideInInspector] public int deathSfxCalled = 0;
    private bool waitingForHitStop;
    private SpriteRenderer sprite;
    private Color color;
    public GameObject mainCam;
    public bool bossLvl;

    #region [HP]
    [Space]
    [Header("HP")]
    public int maxHP;
    private int hp;
    public GameObject hpBar;
    float hpFill;
    public Vector2 checkPoint;
    #endregion

    [Space]
    [Header("Combo Graphics")]
    public Text comboUI;
    public Text comboNamesUI;
    public GameObject[] comboNamesSprites;
    public GameObject[] comboBarMeter;
    float comboFill;

    [Space]
    [Header("Combo Names & Counter")]
    public string[] comboNames;
    public int[] comboNum;

    [Space]
    [Header("VFX")]
    public ParticleSystem jumpFeathers;
    public ParticleSystem hitEffect;
    public ParticleSystem blockHitRight;
    public ParticleSystem blockHitLeft;
    public ParticleSystem dustEffect;
    public ParticleSystem dashEffect;
    public ParticleSystem slashEffectRight;
    public ParticleSystem slashEffectLeft;
    public ParticleSystem feathers;
    public ParticleSystem feathers2;
    public ParticleSystem pOuchR;
    #endregion



    [Space]
    #region [Graphics]
    [Header("Graphics")]
    public GameObject manholeIndicator;
    public GameObject caneEquipped;
    public GameObject manholeEquipped;
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
    public P_ShieldingState shielding { get; private set; }
    public P_DeathState death {  get; private set; }

    #endregion


    #region[Vectors]
    [Header("Forces")]
    public float moveSpeed;
    public float jumpForce;
    public float aerialBounceForce;
    public Vector2[] attackMovement;
    public float normalHitStop;
    public float knockUpHitStop;
    public float forwardHitStop;
    public float counterHitStop;


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
    public float airBorneTime;
    [HideInInspector] public float airBorneCount;
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
        shielding = new P_ShieldingState(this, stateMachine, "Shield");
        death = new P_DeathState(this, stateMachine, "Death");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        defaultGravity = DefaultGravity(0.2f);

    }


    void Start()
    {
        color = sprite.color;
        stateMachine.Initialize(idle);
        caneWpn = true;
        wpnSwapSFXCalled = false;
        manholeAvailable = true;
        caneEquipped.SetActive(false);
        manholeEquipped.SetActive(false);
        counterAttackTutCanvas.SetActive(false);
        hp = maxHP;
        canBeDamaged = true;
    }

    
    void Update()
    {

        stateMachine.currentState.Update();
        CollisionChecks();
        UseSkill();
        WeaponSwap();
        ComboCounterUI();     
        AirBorneNoMovement();
        HpBarFill();    
        Death();

        //code onli for alpha
        /*if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Time.timeScale = 1f;
            counterAttackTutCanvas.SetActive(false);
        }*/
        
        if(comboHits >= comboNum[3] && !feathersOn)
        {
            feathers.Play();
            feathers2.Play();
            shockwaveManager.CallShockwave();
            feathersOn = true;
        }

    }

    public void Damage(int dmg)
    {
        if (!canBeDamaged)
            return;

        hp -= dmg;
       
        SoundManager.PlaySfx(SoundType.PLAYERHURT);

        pOuchR.Play();
            
        StartCoroutine("SpriteHit", 0.2f);
        HitStop(normalHitStop);
        mainCam.GetComponentInChildren<Animator>().SetTrigger("Shake2");

    }

    private void HpBarFill()
    {
        hpFill = (float)hp;
        hpBar.GetComponent<Image>().fillAmount = hpFill/100f;
    }

    private void Death()
    {
        if(hp <= 0 && !bossLvl)
        {
            
            if(checkPoint == null)
            {
                stateMachine.Changestate(death);
                StartCoroutine(pDeathEffect()); //plays death effect
                //this.gameObject.transform.position = new Vector2(0, 0);
                //SetVelocity(0,0);   
            }
            else
            {
                stateMachine.Changestate(death);
                StartCoroutine(pDeathEffect());
                //this.gameObject.transform.position = checkPoint;
                //SetVelocity(0, 0);
            }
        }
        else if(hp <= 0 && bossLvl)
        {
            Time.timeScale = 1f;
            stateMachine.Changestate(death);
            StartCoroutine(pDeathEffect());
        }
    }

    private IEnumerator pDeathEffect() //player dies
    {
        Time.timeScale = 1f;
        deathSfxCalled++;
        if (deathSfxCalled == 1)
        {
            SoundManager.PlaySfx(SoundType.PLAYERDEATH);
            playerDeathVfx.PlayDeathEffect();
        }
        yield return new WaitForSeconds(1f);
        if (bossLvl)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (checkPoint == null)
        {
            SetVelocity(0, -1);
            this.gameObject.transform.position = new Vector2(0, 0);
            stateMachine.Changestate(fall);
        }
        else
        {
            SetVelocity(0, -1);
            this.gameObject.transform.position = checkPoint;
            stateMachine.Changestate(fall);
        }
        playerDeathVfx.ReverseDeathEffect();
        hp = maxHP;
        yield return new WaitForSeconds(5f);
        deathSfxCalled = 0;
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

    IEnumerator SpriteHit(float seconds) //player gets hit
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(seconds);
        sprite.color = color;
    }

    public void AirBorneNoMovement()
    {
        if (airBorneCount > 0)
        {
            SetVelocity(0, rb.linearVelocityY);
        }

        airBorneCount -= Time.deltaTime;
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

    public void KnockBack(float _x, float _y)
    {
        if (cannotBeKnocked)
            return;

        Debug.Log("Knocked");
        rb.linearVelocity = new Vector2(_x * facingDir * -1, _y);
        StartCoroutine("CannotBeKnockedCoroutine", 0.3f);
    }

    IEnumerator CannotBeKnockedCoroutine(float seconds)
    {
        cannotBeKnocked = true;
        yield return new WaitForSeconds(seconds);
        cannotBeKnocked = false;
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
       /* if (Input.GetKeyDown(KeyCode.Mouse1) && !isBusy && isGround && !caneWpn)
        {
            
            SkillManager.instance.manholeSkill.SkillAvailable();
        }*/

        if (Input.GetButtonDown("Fire3"))
        {
            SkillManager.instance.dashSkill.SkillAvailable();
            
        }

        //if((Input.GetKeyUp(KeyCode.Mouse1) && !isBusy && caneWpn && isGround)|| (Input.GetKeyUp(KeyCode.Mouse1) && !isBusy && caneWpn && SkillManager.instance.launchSkill.launchDown))
        if ((Input.GetKeyUp(KeyCode.Mouse1) && !isBusy && caneWpn && isGround))
        {
            SkillManager.instance.launchSkill.jabAttack = true;
            SkillManager.instance.launchSkill.SkillAvailable();
        }
        else if((Input.GetKeyUp(KeyCode.W) && !isBusy && caneWpn && isGround))
        {
            StartCoroutine(SkillManager.instance.launchSkill.launchDirRetain(1f, 1));
            SkillManager.instance.launchSkill.SkillAvailable();
        }

    }

    private void WeaponSwap()
    {

        if (Input.GetKeyDown(KeyCode.E) && !isBusy && manholeAvailable)
        {
            caneWpn = !caneWpn;
            stateMachine.Changestate(shielding);
        }

        if (Input.GetKeyUp(KeyCode.E) && !caneWpn)
        {
            caneWpn = !caneWpn;
            stateMachine.Changestate(idle);
        }

        if (caneWpn)
        {
            caneEquipped.SetActive(true);
            //if (!wpnSwapSFXCalled)
            //{
            //    wpnSwapSFXCalled = true;
            //    SoundManager.PlaySfx(SoundType.PLAYEREQUIPSWAP);
            //}
            manholeEquipped.SetActive(false);
        }
        else
        {
            caneEquipped.SetActive(false);
            //if (wpnSwapSFXCalled)
            //{
            //    wpnSwapSFXCalled = false;
            //    SoundManager.PlaySfx(SoundType.PLAYEREQUIPSWAP);
            //}
            manholeEquipped.SetActive(true);
        }
    }

    //private IEnumerator pWeaponSwap() //player swaps wpn
    //{
    //    SoundManager.PlaySfx(SoundType.PLAYEREQUIPSWAP); // need help
    //    yield return null;

    //}

    private void ComboCounterUI()
    {
        if (comboHitCount < 0)
            comboHits = 0;

        switch (comboHits) 
        {
            case int x when x>= comboNum[0] && x< comboNum[1]:
                comboUpSFXCalledD++;
                PlayComboSFX();
                comboNamesSprites[0].SetActive(true);
                comboNamesSprites[1].SetActive(false);
                comboNamesSprites[2].SetActive(false);
                comboNamesSprites[3].SetActive(false);
                break;
            case int x when x >= comboNum[1] && x < comboNum[2]:
                //comboNamesUI.gameObject.SetActive(true);
                //comboNamesUI.text = comboNames[1];
                comboUpSFXCalledC++;
                PlayComboSFX();
                comboNamesSprites[1].SetActive(true);
                comboNamesSprites[0].SetActive(false);
                comboNamesSprites[2].SetActive(false);
                comboNamesSprites[3].SetActive(false);
                break;
            case int x when x >= comboNum[2] && x < comboNum[3]:
                //comboNamesUI.gameObject.SetActive(true);
                //comboNamesUI.text = comboNames[2];
                comboUpSFXCalledB++;
                PlayComboSFX();
                comboNamesSprites[2].SetActive(true);
                comboNamesSprites[1].SetActive(false);
                comboNamesSprites[0].SetActive(false);
                comboNamesSprites[3].SetActive(false);
                break;
            case int x when x >= comboNum[3]:
                //comboNamesUI.gameObject.SetActive(true);
                //comboNamesUI.text = comboNames[3];
                comboUpSFXCalledR++;
                PlayComboSFX();
                comboNamesSprites[3].SetActive(true);
                comboNamesSprites[1].SetActive(false);
                comboNamesSprites[2].SetActive(false);
                comboNamesSprites[0].SetActive(false);
                //
                comboBarMeter[1].SetActive(false);
                comboBarMeter[2].SetActive(true);
                //
                ravenousVfxManager.PlayRavenousEffect();
                break;
                default:
                ravenousVfxManager.DisableRavenousEffect();
                feathers.Stop();
                feathers2.Stop();
                feathersOn = false;
                comboUpSFXCalledD = 0;
                comboUpSFXCalledB = 0;
                comboUpSFXCalledC = 0;
                comboUpSFXCalledR = 0;
                comboNamesUI.gameObject.SetActive(false);
                comboNamesSprites[0].SetActive(false);
                comboNamesSprites[1].SetActive(false);
                comboNamesSprites[2].SetActive(false);
                comboNamesSprites[3].SetActive(false);
                break;

        }

        if(comboHits == 0)
        {
            comboUI.gameObject.SetActive(false);
            comboBarMeter[0].SetActive(false);
            comboBarMeter[1].SetActive(false);
            comboBarMeter[2].SetActive(false);
        }
        else
        {
            //comboUI.gameObject.SetActive(true);
            comboBarMeter[0].SetActive(true);
            comboBarMeter[1].SetActive(true);
        }

        comboFill = (float)comboHits / (float)comboNum[3];

        //Debug.Log(fill);

        comboBarMeter[1].GetComponent<Image>().fillAmount = comboFill;

        //comboUI.text = $"Combo {comboHits}";

        comboHitCount -= Time.deltaTime;
    }


    public void PlayComboSFX()
    {
        if (comboUpSFXCalledD == 1)
        {
            SoundManager.PlaySfx(SoundType.COMBOUP);
        }
        else if (comboUpSFXCalledC == 1)
        {
            SoundManager.PlaySfx(SoundType.COMBOUP);
        }
        else if (comboUpSFXCalledB == 1)
        {
            SoundManager.PlaySfx(SoundType.COMBOUP);
        }
        else if (comboUpSFXCalledR == 1)
        {
            SoundManager.PlaySfx(SoundType.COMBORAVENOUS);
        }
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("CheckPoint"))
        {
            Debug.Log("Checkpoint found");
            checkPoint = new Vector2(collision.transform.position.x, collision.transform.position.y);
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            SoundManager.PlaySfx(SoundType.PLAYERHURTSPIKES);
            Damage(5);
            KnockBack(3, 10);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(groundChecker.position.x, groundChecker.position.y), new Vector2(groundChecker.position.x, groundChecker.position.y - groundDistance));
        Gizmos.DrawLine(new Vector2(wallChecker.position.x, wallChecker.position.y), new Vector2(wallChecker.position.x + wallDistance * facingDir, wallChecker.position.y));
        Gizmos.DrawWireSphere(new Vector2(meleeAttackChecker.position.x, meleeAttackChecker.position.y), meleeAttackRange);
        //Gizmos.DrawLine(new Vector2(manholeThrowChecker.position.x, manholeThrowChecker.position.y), new Vector2(manholeThrowChecker.position.x + manHoleThrowRange * manHoleAim.xDir, manholeThrowChecker.position.y + manHoleThrowRange*manHoleAim.yDir));
    }
}
