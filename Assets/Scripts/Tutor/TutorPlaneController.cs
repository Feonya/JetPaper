using UnityEngine;
using UnityEngine.UI;

public class TutorPlaneController : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private float groundCheckRadius;
    private bool onGround;

    private enum States { ForcedIdle, Fly };

    private States state;

    public Text scoreText;

    public bool onForcedIdleState;

    public float windforce;

    private Rigidbody2D body;
    private Transform tutorPlaneTransform;

    public TutorWordsController tutorWordsController;

    private void Start()
    {
        groundCheckRadius = 0.4f;
        onGround = false;

        state = States.ForcedIdle;

        onForcedIdleState = true;

        windforce = 1.0f; // 初始风力40

        body = GetComponent<Rigidbody2D>();
        tutorPlaneTransform = GetComponent<Transform>();

        DisablePhysicsSimulate(); // 游戏开始时先关闭物理模拟
    }

    private void FixedUpdate()
    {
        CheckOnGround();
        CheckState();
        CheckAction();

        Fly();
    }

    private void Fly()
    {
        body.velocity = new Vector2(windforce, body.velocity.y);
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

    private void CheckOnGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        Debug.DrawLine(groundCheck.position, groundCheck.position + new Vector3(groundCheckRadius, 0.0f, 0.0f), Color.red);
        Debug.DrawLine(groundCheck.position, groundCheck.position - new Vector3(0.0f, groundCheckRadius, 0.0f), Color.red);
    }

    private void CheckState()
    {
        if (onForcedIdleState)
        {
            state = States.ForcedIdle;
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
        else if (state == States.Fly)
        {
            body.MoveRotation(Mathf.Atan2(body.velocity.y, windforce) * Mathf.Rad2Deg); // 根据速率切换角度

            if (tutorPlaneTransform.position.y < -1.5f)
            {
                if (tutorWordsController.wordsNumber == 17)
                {
                    windforce = 0.0f;
                    body.velocity = Vector2.zero;
                    onForcedIdleState = true;
                    DisableGravity();
                    tutorWordsController.wordsNumber = 18;
                    tutorWordsController.OnWordsClick();
                }
            }
        }
    }

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

    // 禁用重力
    public void DisableGravity()
    {
        if (body.gravityScale != 0.0f)
        {
            body.gravityScale = 0.0f;
        }
    }

    // 启用重力
    public void EnableGravity()
    {
        if (body.gravityScale == 0.0f)
        {
            body.gravityScale = 0.05f;
        }
    }
}