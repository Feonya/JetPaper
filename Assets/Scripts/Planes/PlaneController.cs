using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    #region --- 地面检测使用定义 ---

    //public Transform groundCheck;
    //public LayerMask whatIsGround;
    //private float groundCheckRadius;
    public bool onGround;
    public GameObject ground;

    #endregion --- 地面检测使用定义 ---

    #region --- 状态机使用定义 ---

    private enum States { ForcedIdle, Fly, Dropped, Disappeared };

    private States state;

    #endregion --- 状态机使用定义 ---

    #region --- 三行数据使用定义 ---

    //public Text scoreText;
    //public Text distanceText;
    public Text windforceText;

    #endregion --- 三行数据使用定义 ---

    public bool onForcedIdleState;
    private bool onDisappearedState;

    public float windforce;
    [HideInInspector]
    public WindforceTextUpdater windforceTextUpdater;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Transform planeTransform;
    private float airPower; // 受到吹气的力量

    public LineRenderer line;
    public float groundPositionY;

    private GameObject player;
    private PlayerController playerController;

    private Transform mainCameraTransform;
    private Camera mainCamera;
    private float mainCameraHalfHight;

    public GameObject cats;
    private CatsController catsController;
    private Transform catTransform;
    public bool hasACat;
    public bool hasAEvil;
    public bool onMercy;

    private void Start()
    {
        //groundCheckRadius = 0.4f;
        onGround = false;

        state = States.ForcedIdle;

        onForcedIdleState = true;
        onDisappearedState = false;

        windforce = 1.0f; // 初始风力40(1.0f) ********************************
        windforceTextUpdater = windforceText.GetComponent<WindforceTextUpdater>();
        windforceTextUpdater.CheckWindforce(windforce);

        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        planeTransform = GetComponent<Transform>();
        airPower = 0.0f;

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            groundPositionY = -3.45f;
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            groundPositionY = -5.0f;
        }

        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();

        mainCameraTransform = player.transform.GetChild(0);
        mainCamera = mainCameraTransform.GetComponent<Camera>();

        hasACat = false;
        hasAEvil = false;
        onMercy = false;

        DisablePhysicsSimulate(); // 游戏开始时先关闭物理模拟
    }

    private void FixedUpdate()
    {
        CheckLine();
        //CheckOnGround();
        CheckState();
        CheckAction();

        CheckFinish();

        Fly();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == ground || collision.gameObject.CompareTag("RoadBlock"))
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                var contact = collision.GetContact(i);
                var contactNormal = contact.normal;

                if (contactNormal.y < 1.1f && contactNormal.y > 0.9f)
                {
                    onGround = true;
                }

                //if (contactNormal.x < 1.1f && contactNormal.x > 0.9f)
                //{
                //    var contactPoint = contact.point;
                //    Debug.DrawLine(contactPoint, planeTransform.position, Color.red);
                //    var contactDirection = planeTransform.InverseTransformPoint(contactPoint);

                //    if (contactDirection.y < 0.0f)
                //    {
                //        onGround = true;
                //    }
                //}
            }
        }
    }

    // 检测人物是否到达终点
    private void CheckFinish()
    {
        if (playerController.onFinishLine)
        {
            ForceIdle();

            windforce = 0.0f;

            // 禁用重力，并归零速度
            if (body.gravityScale != 0.0f)
            {
                body.gravityScale = 0.0f;
                body.velocity = Vector2.zero;
            }
        }
    }

    private void CheckLine()
    {
        mainCameraHalfHight = mainCamera.orthographicSize;

        if (planeTransform.position.y >= mainCameraTransform.position.y + mainCameraHalfHight)
        {
            if (!line.enabled)
            {
                line.enabled = true;
            }
            Vector3 planePosition = planeTransform.position;
            Vector3 groundPosition = new Vector3(planePosition.x, groundPositionY, 0.0f);

            line.SetPosition(0, planePosition);
            line.SetPosition(1, groundPosition);
        }
        else
        {
            if (line.enabled)
            {
                line.enabled = false;
            }
        }
    }

    private void Fly()
    {
        body.velocity = new Vector2(windforce, body.velocity.y);
    }

    public void GetPower(float power)
    {
        airPower = power;
    }

    // 吹的气与飞机相撞后向上的推力
    public void PushUp()
    {
        body.AddForce(new Vector2(0.0f, airPower));
    }

    public void ForceIdle()
    {
        if (!onForcedIdleState)
        {
            onForcedIdleState = true;
            DisablePhysicsSimulate(); // 关闭物理模拟
        }
    }

    public void OutOfForcedIdleState()
    {
        onForcedIdleState = false;
        EnablePhysicsSimulate(); // 打开物理模拟
    }

    public void Disappear()
    {
        if (!onDisappearedState)
        {
            onDisappearedState = true;
        }
    }

    public void Appear()
    {
        if (onDisappearedState)
        {
            onDisappearedState = false;
        }
    }

    // 捕获猫
    public void CaptureACat()
    {
        hasACat = true;
        catsController = cats.GetComponentInChildren<CatsController>();
        catTransform = cats.transform.GetChild(0);

        if (catTransform.gameObject.name == "EvilCat")
        {
            hasAEvil = true;
        }
    }

    // 放走猫
    public void ReleaseACat()
    {
        catsController.captured = false;
        if (!onMercy)
        {
            catsController.spriteRenderer.flipX = true;
        }

        // 将释放的猫在层级栏中放置末尾
        catTransform.SetAsLastSibling();
    }

    //private void CheckOnGround()
    //{
    //    onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    //    Debug.DrawLine(groundCheck.position, groundCheck.position + new Vector3(groundCheckRadius, 0.0f, 0.0f), Color.red);
    //    Debug.DrawLine(groundCheck.position, groundCheck.position - new Vector3(0.0f, groundCheckRadius, 0.0f), Color.red);
    //}

    private void CheckState()
    {
        if (onForcedIdleState)
        {
            state = States.ForcedIdle;
        }
        else if (onDisappearedState)
        {
            state = States.Disappeared;
        }
        else if (onGround)
        {
            state = States.Dropped;
        }
        else if (!onGround)
        {
            state = States.Fly;
        }
    }

    private void CheckAction()
    {
        if (onForcedIdleState)
        {
            // 啥都不做
        }
        else if (state == States.Disappeared)
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }

            windforce = 0.0f;

            // 如果没有猫
            if (!hasACat)
            {
                playerController.Dead();
            }
            else
            {
                if (!onMercy)
                {
                    onMercy = true;
                    catsController.CallDoMercy();
                }
            }
        }
        else if (state == States.Dropped)
        {
            if (windforce > 0.0f)
            {
                windforce -= 3.0f * Time.fixedDeltaTime;
            }
            else
            {
                windforce = 0.0f;
            }

            if (body.velocity.y > 0.5f)
            {
                body.velocity = new Vector2(windforce, 0.5f);
            }

            // 如果人物没有到达终点
            if (!playerController.onFinishLine)
            {
                // 如果没有猫
                if (!hasACat)
                {
                    playerController.Dead();
                }
                else
                {
                    if (!onMercy)
                    {
                        onMercy = true;
                        catsController.CallDoMercy();
                    }
                }
            }
        }
        else if (state == States.Fly)
        {
            //body.MoveRotation(Mathf.Atan2(body.velocity.y, windforce) * Mathf.Rad2Deg); // 根据速率切换角度
            //planeTransform.right = new Vector3(windforce, body.velocity.y, 0.0f);
            body.rotation = Mathf.Atan2(body.velocity.y, windforce) * Mathf.Rad2Deg;

            if (Random.Range(1, 1001) == 500) // 1%1000的几率重新生成随机风力
            {
                windforce = Random.Range(0.5f, 3.0f); // 随机风力，值为浮点，范围0.5~3.0
                windforceTextUpdater.CheckWindforce(windforce);
            }
        }
    }

    // 飞机落地后的被猫复活
    public void ReviveWithCat(Sprite planeSprite, Vector3 catPosition)
    {
        spriteRenderer.sprite = planeSprite; // 飞机外形变化
        planeTransform.position = catPosition; // 飞机位置位于猫的最后位置
        onGround = false;
        if (!catsController.captured)
        {
            hasACat = false;
        }
        onMercy = false;
        Appear();

        // 风力的重置
        windforce = 1.0f;
        windforceTextUpdater.CheckWindforce(windforce);

        body.velocity = Vector2.zero;
    }

    /*
    // 禁用重力
    private void DisableGravity()
    {
        if (body.gravityScale != 0.0f)
        {
            body.gravityScale = 0.0f;
        }
    }

    // 启用重力
    private void EnableGravity()
    {
        if (body.gravityScale == 0.0f)
        {
            body.gravityScale = 0.05f;
        }
    }
    */

    // 禁用物理模拟
    public void DisablePhysicsSimulate()
    {
        if (body.simulated)
        {
            body.simulated = false;
        }
    }

    // 启用物理模拟
    public void EnablePhysicsSimulate()
    {
        if (!body.simulated)
        {
            body.simulated = true;
        }
    }
}