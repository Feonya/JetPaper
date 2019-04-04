using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region --- 地面检测使用定义 ---

    public Transform groundCheck;
    public LayerMask whatIsGround;
    protected float groundCheckRadius;
    public bool onGround;
    public bool onRough;

    public SurfaceChecker surfaceChecker;

    #endregion --- 地面检测使用定义 ---

    #region --- 移动、跳跃使用定义 ---

    public bool canMove;
    public float speed; // 移动速度
    protected float sensitivity;
    protected float maxSpeed; // 最大移动速度
    protected float jumpForce; // 跳跃推力
    protected bool canJump;

    #endregion --- 移动、跳跃使用定义 ---

    #region --- 吸气、吐气使用定义 ---

    public GameObject air;
    protected bool onInhaleState;
    protected bool onBlowState;

    public float powerValue;
    public float powerValueIncrement; // 聚气时的增长幅度
    public float powerBarAmountIncrement; // 聚气时powerbar对应的增长幅度

    #endregion --- 吸气、吐气使用定义 ---

    #region --- 状态机使用定义 ---

    protected enum States { ForcedIdle, Idle, Move, Jump, Inhale, Blow, Dead, Loved, Tripped, Shitted }; // 状态机枚举

    protected States state; // 当前状态

    #endregion --- 状态机使用定义 ---

    protected bool onForcedIdleState;
    public bool onDeadState;
    protected bool onLovedState;
    public bool onTrippedState;
    public bool onShittedState;

    public GameObject rebirthConfirm;

    public Canvas hud;
    protected HUDCanvasController hudCanvasController;
    public Button jumpButton;
    private EventTrigger jumpButtonEventTrigger;
    public Button blowButton;
    private EventTrigger blowButtonEventTrigger;

    public Rigidbody2D body;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected AnimatorStateInfo animatorInfo;
    private Transform playerTransform;

    public GameObject plane;
    private Transform planeTransform;
    private PlaneController planeController;
    private Rigidbody2D planeBody;

    public AudioSource jumpSound;
    public AudioSource blowSound;
    public AudioSource deadSound;
    public AudioSource ohYeahSound;
    public AudioSource eatShitSound;
    public AudioSource ahSound;

    private float rebirthPositionX;
    private float rebirthPositionY; // y为-2.96时，玩家在地面上

    // 用于保存有人或飞机落入的坑
    public GameObject pitWithPlayer;
    public GameObject pitWithPlane;

    #region 更新路程及分数的定义

    public Text distanceText;
    private StringBuilder distanceTextString;
    private string distanceValueString;
    private string oldDistanceValueString;
    private float distanceNumber;
    private float distanceNumberAddOne;
    private float distanceNumberMinusOne;

    public Text scoreText;
    private StringBuilder scoreTextString;
    private string scoreValueString;
    private string oldScoreValueString;
    public int scoreNumber;

    #endregion 更新路程及分数的定义

    public bool onFinishLine;
    public AudioSource mainMusic;
    public AudioSource finishMusic;

    public GameObject didNotPlayButton;
    private DidNotPlayButtonController didNotPlayButtonController;

    //public GameObject debugRebirthButton; // ********************************测试用

    protected void Awake()
    {
        groundCheckRadius = 0.3f;
        onGround = true;
        onRough = false;

        canMove = true;
        speed = 0.0f;
        sensitivity = 40.0f;
        maxSpeed = 5.0f;
        jumpForce = 650.0f;
        canJump = true;

        onInhaleState = false;
        onBlowState = false;

        powerValue = 50.0f;
        powerValueIncrement = (120.0f / 100.0f) * 2.0f;
        powerBarAmountIncrement = (120.0f / 100.0f) * 0.01f;

        state = States.ForcedIdle;

        onForcedIdleState = true;
        onDeadState = false;
        onLovedState = false;
        onTrippedState = false;
        onShittedState = false;

        jumpButtonEventTrigger = jumpButton.GetComponent<EventTrigger>();
        blowButtonEventTrigger = blowButton.GetComponent<EventTrigger>();

        body = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        playerTransform = gameObject.transform;

        planeTransform = plane.GetComponent<Transform>();
        planeController = plane.GetComponent<PlaneController>();
        planeBody = plane.GetComponent<Rigidbody2D>();

        hudCanvasController = hud.GetComponent<HUDCanvasController>();

        rebirthPositionX = 0.0f;
        rebirthPositionY = 0.0f;

        // 距离相关
        distanceTextString = new StringBuilder("路程：0");
        oldDistanceValueString = "0";
        distanceNumber = 0.0f;
        distanceNumberAddOne = 3.0f; // x轴的3个单位为路程的1个单位
        distanceNumberMinusOne = -3.0f;

        // 分数相关
        scoreTextString = new StringBuilder("分数：0");
        oldScoreValueString = "0";
        scoreNumber = 0;

        onFinishLine = false;

        didNotPlayButtonController = didNotPlayButton.GetComponent<DidNotPlayButtonController>();
    }

    // 初始化
    protected void Start()
    {
        DisableBlowButton();
        DisableJumpButton();
    }

    // 主循环
    private void FixedUpdate()
    {
        CheckOnGround();
        CheckState();
        CheckAnim();
        CheckAction();

        CheckFinish();

        Move(canMove);

        CheckDistance();
    }

    // 执行移动
    private void Move(bool canMove)
    {
        if (canMove)
        {
            if (onRough) //如果再粗糙的地面...
            {
                speed = speed * 0.5f;
            }

            body.velocity = new Vector2(speed, body.velocity.y);
        }
    }

    // 根据手机x加速获得移动速度
    protected void GetAccX()
    {
        speed = Input.acceleration.x * sensitivity;
        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

        #region 测试用，键盘控制速度

        speed += Input.GetAxis("Horizontal") * 4.0f;

        #endregion 测试用，键盘控制速度
    }

    // 检测是否到达终点
    private void CheckFinish()
    {
        if (distanceNumber >= 300.0f)
        {
            speed = 0.0f;
            body.velocity = new Vector2(0.0f, body.velocity.y);
            if (!onFinishLine && onGround)
            {
                didNotPlayButtonController.DisableDidNotPlayButton();
                DisableBlowButton();
                DisableJumpButton();

                onFinishLine = true;
                mainMusic.Stop(); // 停止主音乐
                finishMusic.PlayDelayed(2.0f); // 两秒后播放结束音乐
            }
        }

        if (onFinishLine)
        {
            ForceIdle();
        }
    }

    // 检查路程
    private void CheckDistance()
    {
        distanceNumber = playerTransform.position.x;

        if (distanceNumber >= distanceNumberAddOne)
        {
            distanceValueString = ((int)(distanceNumberAddOne / 3.0f)).ToString();
            distanceText.text = distanceTextString.Replace(oldDistanceValueString, distanceValueString).ToString();
            oldDistanceValueString = distanceValueString;

            distanceNumberAddOne += 3.0f;
            distanceNumberMinusOne += 3.0f;

            UpdateScore(1);
        }
        else if (distanceNumber <= distanceNumberMinusOne)
        {
            distanceValueString = ((int)(distanceNumberMinusOne / 3.0f)).ToString();
            distanceText.text = distanceTextString.Replace(oldDistanceValueString, distanceValueString).ToString();
            oldDistanceValueString = distanceValueString;

            distanceNumberAddOne -= 3.0f;
            distanceNumberMinusOne -= 3.0f;

            UpdateScore(-1);
        }

        if (distanceValueString == "30")
        {
            TalkingDataController.DistanceGreaterThan30 = true;
        }
    }

    // 加分
    public void UpdateScore(int n)
    {
        scoreNumber += n;

        scoreValueString = scoreNumber.ToString();
        scoreText.text = scoreTextString.Replace(oldScoreValueString, scoreValueString).ToString();
        oldScoreValueString = scoreValueString;
    }

    // 执行跳跃并进入跳跃状态
    public virtual void Jump()
    {
        if (onGround && canJump)
        {
            body.AddForce(new Vector2(0.0f, jumpForce));

            jumpSound.Play();
        }
    }

    // 进入吸气状态
    public void Inhale()
    {
        if (!onBlowState && onGround)
        {
            onInhaleState = true;
        }
    }

    // 进入吹气状态
    public void Blow()
    {
        if (onInhaleState)
        {
            onInhaleState = false;
            onBlowState = true;

            blowSound.Play();

            air.SetActive(true);
        }
    }

    // 吹气动画结束后调用此方法
    protected void DisappearAir()
    {
        air.SetActive(false);
        //air.GetComponent<CapsuleCollider2D>().enabled = true;

        hudCanvasController.ResetPower();
    }

    // 强制原地状态
    public void ForceIdle()
    {
        if (!onForcedIdleState)
        {
            onForcedIdleState = true;
        }
    }

    // 退出强制原地状态
    public void OutOfForcedIdleState()
    {
        if (onForcedIdleState)
        {
            onForcedIdleState = false;
        }
    }

    // 进入死亡状态
    public void Dead()
    {
        if (!onDeadState)
        {
            onDeadState = true;
            ahSound.Play();
            //deadSound.Play(44100);
            deadSound.PlayDelayed(0.5f);

            TalkingDataController.DeadTimes += 1;
        }
    }

    // 复活
    public void Rebirth()
    {
        if (onDeadState)
        {
            //debugRebirthButton.SetActive(false); // ********************************测试用

            // 根据飞机和人物的前后确定复活位置
            if (playerTransform.position.x <= planeTransform.position.x) // 从人物位置复活
            {
                rebirthPositionX = playerTransform.position.x;

                // 如果人物在坑里...
                if (pitWithPlayer != null)
                {
                    rebirthPositionX -= 1.5f;
                }
            }
            else // 从飞机位置复活
            {
                rebirthPositionX = planeTransform.position.x;

                // 如果飞机在坑里...
                if (pitWithPlane != null)
                {
                    rebirthPositionX -= 1.5f;
                }
            }

            // 如果坑中有东西，隐藏坑中的假体
            if (pitWithPlayer != null) // 坑中有人
            {
                pitWithPlayer.GetComponent<PitController>().DisableFakePlayer();
                pitWithPlayer = null;
            }
            if (pitWithPlane != null) // 坑中有飞机
            {
                pitWithPlane.GetComponent<PitController>().DisableFakePlane();
                pitWithPlane = null;
            }


            rebirthPositionY = surfaceChecker.CheckSurface(rebirthPositionX).y + 0.54f; // 判断地表高度决定复活的y轴高度

            // 恢复player位置
            playerTransform.position = new Vector2(rebirthPositionX, rebirthPositionY);

            // 如果player处于消失状态...
            if (!spriteRenderer.enabled)
            {
                spriteRenderer.enabled = true;
                body.simulated = true;
            }

            // 如果飞机没有在猫的升天状态之下
            if (!planeController.onMercy)
            {
                // 恢复plane的位置
                planeTransform.position = new Vector2(rebirthPositionX, rebirthPositionY + 5.0f);
                planeController.onGround = false;

                // 如果plane处于消失状态...
                if (!plane.activeSelf)
                {
                    plane.SetActive(true);
                    planeController.Appear();
                }

                // 如果plane在强制停止状态
                if (planeController.onForcedIdleState)
                {
                    planeController.OutOfForcedIdleState();
                }

                // 恢复风力
                planeController.windforce = 1.0f;
                planeController.windforceTextUpdater.CheckWindforce(planeController.windforce);

                // 恢复飞机的垂直速度
                planeBody.velocity = Vector2.zero;
            }

            // 退出死亡状态
            animator.SetBool("isDead", false);
            onDeadState = false;

            // 恢复人物可移动
            canMove = true;

            // 恢复人物重力
            EnableGravity();

            // 减分数
            UpdateScore(-10);

            // 聚力槽归零
            hudCanvasController.ResetPower();
        }
    }

    // 消失、隐身
    public void Disappear()
    {
        spriteRenderer.enabled = false;
        body.simulated = false;
    }

    // 进入被迷住状态
    public virtual void Love()
    {
        if (!onLovedState && !onDeadState)
        {
            onLovedState = true;
            ohYeahSound.Play();
        }
    }

    // 脱离被迷住状态
    public void DoNotLove()
    {
        if (onLovedState)
        {
            onLovedState = false;
        }
    }

    // 进入摔倒状态
    public void Trip()
    {
        if (!onTrippedState)
        {
            onTrippedState = true;
        }
    }

    // 推力摔倒状态，站起来
    public void StandUp()
    {
        if (onTrippedState)
        {
            onTrippedState = false;
            spriteRenderer.flipX = false;
        }
    }

    // 进入被恶心状态
    public virtual void EatShit()
    {
        if (!onShittedState && !onDeadState)
        {
            onShittedState = true;
            eatShitSound.Play();
        }
    }

    // 离开被恶心状态
    public void DoNotEatShit()
    {
        if (onShittedState)
        {
            onShittedState = false;
        }
    }

    #region --- 主循环中使用的函数 ---

    // 判断是否在地面
    private void CheckOnGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        Debug.DrawLine(groundCheck.position, groundCheck.position + new Vector3(groundCheckRadius, 0.0f, 0.0f), Color.red);
        Debug.DrawLine(groundCheck.position, groundCheck.position - new Vector3(0.0f, groundCheckRadius, 0.0f), Color.red);
    }

    // 判断状态机
    private void CheckState()
    {
        // 死亡
        if (onDeadState)
        {
            state = States.Dead;
        }
        // 吃到屎
        else if (onShittedState)
        {
            state = States.Shitted;
        }
        // 摔倒
        else if (onTrippedState)
        {
            state = States.Tripped;
        }
        // 被迷住
        else if (onLovedState)
        {
            state = States.Loved;
        }
        // 强制原地
        else if (onForcedIdleState)
        {
            state = States.ForcedIdle;
        }
        // 吸气
        else if (onInhaleState)
        {
            state = States.Inhale;
        }
        // 吹气
        else if (onBlowState)
        {
            state = States.Blow;
        }
        // 跳跃
        else if (!onGround)
        {
            state = States.Jump;
        }
        // 移动
        else if (speed != 0 && onGround)
        {
            state = States.Move;
        }
        // 原地
        else if (speed == 0 && onGround)
        {
            state = States.Idle;
        }
    }

    // 根据状态机判断动画
    private void CheckAnim()
    {
        // 死亡
        if (state == States.Dead)
        {
            if (!animator.GetBool("isDead"))
            {
                animator.SetBool("isDead", true);
                animator.SetBool("idling", false);
                animator.SetBool("moving", false);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
                animator.SetBool("loving", false);
                animator.SetBool("tripping", false);
                animator.SetBool("shitting", false);
            }
        }
        // 吃到屎
        else if (state == States.Shitted)
        {
            if (!animator.GetBool("shitting"))
            {
                animator.SetBool("shitting", true);
                animator.SetBool("idling", false);
                animator.SetBool("moving", false);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
            }
        }
        // 摔倒
        else if (state == States.Tripped)
        {
            if (!animator.GetBool("tripping"))
            {
                animator.SetBool("tripping", true);
                animator.SetBool("idling", false);
                animator.SetBool("moving", false);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
            }
        }
        // 被迷住
        else if (state == States.Loved)
        {
            if (!animator.GetBool("loving"))
            {
                animator.SetBool("loving", true);
                animator.SetBool("idling", false);
                animator.SetBool("moving", false);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
            }
        }
        // 强制原地
        else if (state == States.ForcedIdle)
        {
            if (!animator.GetBool("idling"))
            {
                animator.SetBool("idling", true);
                animator.SetBool("moving", false);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
                animator.SetBool("loving", false);
                animator.SetBool("tripping", false);
                animator.SetBool("shitting", false);
            }
        }
        // 吸气
        else if (state == States.Inhale)
        {
            if (!animator.GetBool("inhaling"))
            {
                animator.SetBool("inhaling", true);
                animator.SetBool("blowing", false);
                animator.SetBool("jumping", false);
                animator.SetBool("moving", false);
                animator.SetBool("idling", false);
                animator.SetBool("loving", false);
                animator.SetBool("tripping", false);
                animator.SetBool("shitting", false);
            }
        }
        // 吹气
        else if (state == States.Blow)
        {
            if (!animator.GetBool("blowing"))
            {
                animator.SetBool("blowing", true);
                animator.SetBool("inhaling", false);
                animator.SetBool("jumping", false);
                animator.SetBool("moving", false);
                animator.SetBool("idling", false);
                animator.SetBool("loving", false);
                animator.SetBool("tripping", false);
                animator.SetBool("shitting", false);
            }
        }
        // 跳跃
        else if (state == States.Jump)
        {
            if (!animator.GetBool("jumping"))
            {
                animator.SetBool("jumping", true);
                animator.SetBool("moving", false);
                animator.SetBool("idling", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
                animator.SetBool("loving", false);
                animator.SetBool("tripping", false);
                animator.SetBool("shitting", false);
            }
        }
        // 移动
        else if (state == States.Move)
        {
            if (!animator.GetBool("moving"))
            {
                animator.SetBool("moving", true);
                animator.SetBool("idling", false);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
                animator.SetBool("loving", false);
                animator.SetBool("tripping", false);
                animator.SetBool("shitting", false);
            }
        }
        // 原地
        else if (state == States.Idle)
        {
            if (!animator.GetBool("idling"))
            {
                animator.SetBool("idling", true);
                animator.SetBool("moving", false);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
                animator.SetBool("loving", false);
                animator.SetBool("tripping", false);
                animator.SetBool("shitting", false);
            }
        }
    }

    // 根据状态机判断行为
    protected virtual void CheckAction()
    {
        // 死亡
        if (state == States.Dead)
        {
            if (onGround)
            {
                speed = 0.0f;
                body.velocity = new Vector2(0.0f, body.velocity.y);
            }

            // 使气体消失
            if (air.activeSelf)
            {
                DisappearAir();
            }

            // 退出其他状态
            if (onForcedIdleState)
            {
                onForcedIdleState = false;
            }
            if (onLovedState)
            {
                onLovedState = false;
            }
            if (onTrippedState)
            {
                onTrippedState = false;
            }
            if (onShittedState)
            {
                onShittedState = false;
            }
            if (onInhaleState)
            {
                onInhaleState = false;
            }
            if (onBlowState)
            {
                onBlowState = false;
            }

            DisableBlowButton();
            DisableJumpButton();

            // 激活广告展示倒计时
            if (!deadSound.isPlaying)
            {
                if (!rebirthConfirm.activeSelf)
                {
                    rebirthConfirm.SetActive(true);
                }
            }
        }
        // 被恶心
        else if (state == States.Shitted)
        {
            if (onGround)
            {
                speed = 0.0f;
            }

            // 使气体消失
            if (air.activeSelf)
            {
                DisappearAir();
            }

            if (onInhaleState)
            {
                onInhaleState = false;
            }
            if (onBlowState)
            {
                onBlowState = false;
            }

            DisableBlowButton();
            DisableJumpButton();
        }
        // 摔倒
        else if (state == States.Tripped)
        {
            if (onGround)
            {
                if (speed >= 0.0f)
                {
                    speed = 2.0f;
                }
                else
                {
                    speed = -2.0f;
                    spriteRenderer.flipX = true;
                }
            }

            // 使气体消失
            if (air.activeSelf)
            {
                DisappearAir();
            }

            if (onInhaleState)
            {
                onInhaleState = false;
            }
            if (onBlowState)
            {
                onBlowState = false;
            }

            DisableBlowButton();
            DisableJumpButton();
        }
        // 被迷住
        else if (state == States.Loved)
        {
            if (onGround)
            {
                speed = 0.0f;
            }

            // 使气体消失
            if (air.activeSelf)
            {
                DisappearAir();
            }

            if (onInhaleState)
            {
                onInhaleState = false;
            }
            if (onBlowState)
            {
                onBlowState = false;
            }

            DisableBlowButton();
            DisableJumpButton();
        }
        // 强制原地
        else if (state == States.ForcedIdle)
        {
            speed = 0.0f;
            body.velocity = new Vector2(0.0f, 0.0f);

            if (onInhaleState)
            {
                onInhaleState = false;
            }
            if (onBlowState)
            {
                onBlowState = false;
            }

            // 使气体消失
            if (air.activeSelf)
            {
                DisappearAir();
            }
        }
        // 吸气
        else if (state == States.Inhale)
        {
            canJump = false;
            speed = 0.0f;
            hudCanvasController.IncreasePower();

            DisableJumpButton();
        }
        // 吹气
        else if (state == States.Blow)
        {
            speed = 0.0f;
            animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
            // 判断动画是否结束... (normalizedTime是一个0~1的浮点数，0代表动画的开头，1表示动画的结尾)
            if (animatorInfo.IsName("PlayerBlow") && animatorInfo.normalizedTime >= 1.0f)
            {
                onBlowState = false;
                canJump = true;
            }

            DisableBlowButton();
            DisableJumpButton();
        }
        // 跳跃
        else if (state == States.Jump)
        {
            GetAccX();

            if (canJump)
            {
                canJump = false;
            }

            DisableBlowButton();
        }
        // 移动
        else if (state == States.Move)
        {
            GetAccX();

            if (body.velocity.y <= 0.0f)
            {
                if (!canJump)
                {
                    canJump = true;
                }

                EnableBlowButton();
                EnableJumpButton();
            }
        }
        // 原地
        else if (state == States.Idle)
        {
            GetAccX();

            if (body.velocity.y <= 0.0f)
            {
                if (!canJump)
                {
                    canJump = true;
                }

                EnableBlowButton();
                EnableJumpButton();
            }
        }
    }

    #endregion --- 主循环中使用的函数 ---

    //// 启用物理模拟
    //public void EnablePhysics()
    //{
    //    if (!body.simulated)
    //    {
    //        body.simulated = true;
    //    }
    //}

    //// 禁用物理模拟
    //public void DisablePhysics()
    //{
    //    if (body.simulated)
    //    {
    //        body.simulated = false;
    //    }
    //}

    // 禁用重力
    public void DisableGravity()
    {
        if (body.gravityScale != 0.0f)
        {
            body.gravityScale = 0.0f;
        }
    }

    // 启用重力
    public virtual void EnableGravity()
    {
        if (body.gravityScale != 4.0f)
        {
            body.gravityScale = 4.0f;
        }
    }

    // 吹气按钮禁用
    public void DisableBlowButton()
    {
        if (blowButton.interactable == true)
        {
            blowButton.interactable = false;
            blowButtonEventTrigger.enabled = false;
        }
    }

    // 吹气按钮激活
    public void EnableBlowButton()
    {
        if (blowButton.interactable == false)
        {
            blowButton.interactable = true;
            blowButtonEventTrigger.enabled = true;
        }
    }

    // 跳跃按钮禁用
    public void DisableJumpButton()
    {
        if (jumpButton.interactable == true)
        {
            jumpButton.interactable = false;
            jumpButtonEventTrigger.enabled = false;
        }
    }

    // 跳跃按钮激活
    public void EnableJumpButton()
    {
        if (jumpButton.interactable == false)
        {
            jumpButton.interactable = true;
            jumpButtonEventTrigger.enabled = true;
        }
    }
}