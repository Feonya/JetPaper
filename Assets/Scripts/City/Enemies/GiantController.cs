using UnityEngine;

public class GiantController : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;
    private PlayerController playerController;

    public GameObject leftLeg;
    private Transform leftLegTransform;
    public GameObject rightLeg;
    private Transform rightLegTransform;

    public GameObject leftShock;
    private Rigidbody2D leftShockBody;
    public GameObject rightShock;
    private Rigidbody2D rightShockBody;
    public LayerMask whatIsCollider;
    private bool onCollidePlayer;

    private bool onLeftLegMove;
    private bool onRightLegMove;

    public AudioSource giantSound;

    private Animator animator;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();

        leftLegTransform = leftLeg.GetComponent<Transform>();
        rightLegTransform = rightLeg.GetComponent<Transform>();

        leftShockBody = leftShock.GetComponent<Rigidbody2D>();
        rightShockBody = rightShock.GetComponent<Rigidbody2D>();
        onCollidePlayer = false;

        onLeftLegMove = true;
        onRightLegMove = false;

        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
    }

    private void FixedUpdate()
    {
        CheckPlayerCollide();
        CheckPlayerInRange();
    }

    public void MoveLeftLeg()
    {
        animator.Play("GiantMoveLeftLeg");
        onRightLegMove = false;
        onLeftLegMove = true;
        giantSound.Play();
    }

    public void MoveRightLeg()
    {
        animator.Play("GiantMoveRightLeg");
        onRightLegMove = true;
        onLeftLegMove = false;
        giantSound.Play();
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (leftLegTransform.position.x - playerTransform.position.x < -10.0f) // 如果玩家在后方10个单位之后
        {
            Destroy(gameObject);
        }
        else if (leftLegTransform.position.x - playerTransform.position.x < 10.0f) // 如果玩家进入前方10个单位
        {
            if (!animator.enabled)
            {
                animator.enabled = true;
            }

            if (onLeftLegMove)
            {
                leftLegTransform.position -= new Vector3(0.01f, 0.0f, 0.0f);
            }
            else
            {
                rightLegTransform.position -= new Vector3(0.01f, 0.0f, 0.0f);
            }

            if (onCollidePlayer)
            {
                playerController.Dead();
            }
        }
    }

    // 检测玩家碰撞
    private void CheckPlayerCollide()
    {
        onCollidePlayer = leftShockBody.IsTouchingLayers(whatIsCollider) ||
                          rightShockBody.IsTouchingLayers(whatIsCollider);
    }
}