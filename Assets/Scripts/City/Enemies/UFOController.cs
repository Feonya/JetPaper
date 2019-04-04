using System.Collections;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    private Animator animator;
    private Transform ufoTransform;

    public AudioSource ufoSound;
    public AudioSource laserSound;

    private GameObject player;
    private Transform playerTransform;
    private PlayerController playerController;

    public GameObject laser;
    private Rigidbody2D laserBody;
    private Transform laserTransform;

    private bool alive;

    private bool onPlayerCaptured;

    private float laserDisappearPositionY; // -2.96表示地面的位置

    private int appearNumber;

    public LayerMask whatIsPlayer;
    private bool onCollidePlayer;

    private void Start()
    {
        ufoTransform = transform;
        animator = GetComponent<Animator>();
        animator.enabled = false;

        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.transform;
        playerController = player.GetComponent<PlayerController>();

        laserBody = laser.GetComponent<Rigidbody2D>();
        laserTransform = laser.transform;
        laser.SetActive(false);

        alive = false;

        onPlayerCaptured = false;

        appearNumber = 5; // 可出现次数

        onCollidePlayer = false;

        laserDisappearPositionY = -2.96f;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();
        CheckCollidePlayer();

        if (alive)
        {
            if (!animator.enabled)
            {
                animator.enabled = true;
                ufoSound.Play(); // 第一次出现时发声
            }
        }
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (ufoTransform.position.x <= playerTransform.position.x) // 如果玩家进入UFO下方
        {
            if (!alive)
            {
                alive = true;
            }
        }

        if (laserTransform.position.y <= laserDisappearPositionY) // 如果激光到达地面
        {
            if (laser.activeSelf)
            {
                laser.SetActive(false);
            }
        }
    }

    // 检测碰撞
    private void CheckCollidePlayer()
    {
        onCollidePlayer = laserBody.IsTouchingLayers(whatIsPlayer);

        if (onCollidePlayer && !onPlayerCaptured)
        {
            playerController.DisableBlowButton();
            playerController.DisableJumpButton();

            playerController.DisableGravity(); // 禁用重力
            playerController.ForceIdle(); // 强制原地

            playerTransform.position = laserTransform.position;

            // 如果激光将玩家运至了ufo的发射口位置
            if (laserTransform.position.y >= ufoTransform.position.y - 0.78f)
            {
                laserBody.velocity = new Vector2(0.0f, 0.0f);
                playerController.Dead();
                onPlayerCaptured = true;
            }
            else
            {
                laserBody.velocity = new Vector2(0.0f, 4.0f);
            }
        }
        else if (!onCollidePlayer && onPlayerCaptured)
        {
            Phase();
            onPlayerCaptured = false;
        }
    }

    // 变换位置
    private void Phase()
    {
        if (!onCollidePlayer)
        {
            // 相位前判断可出现次数是否用完
            if (appearNumber <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                appearNumber -= 1;
            }

            // 计算随机位置
            float randX = playerTransform.position.x + Random.Range(0.0f, 4.0f);
            float randY = Random.Range(0.0f, 7.0f);

            ufoTransform.position = new Vector2(randX, randY);
            animator.Play("UFOAppear");
            ufoSound.Play();
        }
    }

    // 发射激光
    private void Lasing()
    {
        if (!laser.activeSelf)
        {
            laser.SetActive(true);
        }

        laserTransform.position = ufoTransform.position - new Vector3(0.0f, 0.78f, 0.0f); // 发射前将激光归位到发射口

        animator.Play("UFOLase");
        laserBody.velocity = new Vector2(0.0f, -8.0f);

        StartCoroutine(CallPhase());
    }

    // 延迟3秒后调用phase()
    private IEnumerator CallPhase()
    {
        yield return new WaitForSeconds(3.0f);

        Phase();
    }
}