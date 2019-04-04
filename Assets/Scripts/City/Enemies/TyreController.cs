using UnityEngine;

public class TyreController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    public GameObject plane;
    private Rigidbody2D planeBody;

    private Rigidbody2D body;
    public AudioSource tyreSound;

    public Transform colliderCheck;
    public LayerMask whatIsCollider;
    public LayerMask whatIsCollider2;
    public LayerMask whatIsGround;
    private float colliderCheckRadius;
    private bool collidedPlayer;
    private bool collidedPlane;
    private bool firstCollidePlane;
    private bool onGround;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;

        colliderCheckRadius = 0.5f;
        collidedPlayer = false;
        collidedPlane = false;
        firstCollidePlane = true; // 真值保持到第一次撞到飞机，之后变为假
        onGround = false;

        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        planeBody = plane.GetComponent<Rigidbody2D>();

        body = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckCollide();
        CheckPlayerInRange();
    }

    private void CheckCollide()
    {
        collidedPlayer = Physics2D.OverlapCircle(colliderCheck.position, colliderCheckRadius, whatIsCollider);
        collidedPlane = Physics2D.OverlapCircle(colliderCheck.position, colliderCheckRadius, whatIsCollider2);
        onGround = Physics2D.OverlapCircle(colliderCheck.position, colliderCheckRadius, whatIsGround);

        Debug.DrawLine(colliderCheck.position, colliderCheck.position + new Vector3(colliderCheckRadius, 0.0f, 0.0f), Color.red);
        Debug.DrawLine(colliderCheck.position, colliderCheck.position - new Vector3(0.0f, colliderCheckRadius, 0.0f), Color.red);
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (transform.position.x - player.transform.position.x < -10.0f) // 如果玩家在后方10个单位之后
        {
            gameObject.SetActive(false);
        }
        else if (transform.position.x - player.transform.position.x < 10.0f) // 如果玩家进入前方10个单位
        {
            // 激活物理模拟
            if (!body.simulated)
            {
                body.simulated = true;
            }
            // 如果撞到玩家...
            if (collidedPlayer)
            {
                playerController.Dead();
            }
            // 如果撞到飞机，并且是第一次撞到...
            if (collidedPlane && firstCollidePlane)
            {
                firstCollidePlane = false;
                planeBody.velocity += new Vector2(0.0f, -2.0f);
            }
            // 如果落地
            if (onGround)
            {
                if (!tyreSound.isPlaying)
                {
                    tyreSound.Play();
                }
            }

            body.velocity = new Vector3(-1.0f, body.velocity.y, 0.0f);
        }
    }
}