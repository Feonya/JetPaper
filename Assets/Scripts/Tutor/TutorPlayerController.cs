using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorPlayerController : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    private bool onGround;

    private AnimatorStateInfo animatorInfo;
    private Animator animator;
    public Rigidbody2D body;
    private Transform tutorPlayerTransform;

    public float speed;
    private float maxSpeed;

    public Button jumpButton;
    private EventTrigger jumpButtonEventTrigger;
    public Button blowButton;
    private EventTrigger blowButtonEventTrigger;

    public bool free;
    public bool canMove;
    public bool canJump;
    private float jumpForce;

    public TutorWordsController tutorWordsController;

    public GameObject air;
    private bool onInhaleState;
    private bool onBlowState;

    public GameObject powerBar;
    private float powerBarAmount; // 力量条的显示，值0~1
    private float powerBarAmountIncrement;
    private float powerValue; // 力量条对于的吹力，值0~120（50~170）
    private float powerValueIncrement;
    private Image powerValueImage;

    public Text scoreText;
    private int scoreNumber;

    public Text distanceText;
    private float distanceNumber;
    private float distanceNumberAddOne;
    private float distanceNumberMinusOne;

    public AudioSource nextWordsSound;
    public AudioSource jumpSound;
    public AudioSource blowSound;

    private void Start()
    {
        onGround = true;

        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        tutorPlayerTransform = transform;

        speed = 0.0f;
        maxSpeed = 4.0f;

        jumpButtonEventTrigger = jumpButton.GetComponent<EventTrigger>();
        blowButtonEventTrigger = blowButton.GetComponent<EventTrigger>();

        DisableJumpButton();
        DisableBlowButton();

        free = false;
        canMove = false;
        canJump = true;
        jumpForce = 650.0f;

        onInhaleState = false;
        onBlowState = false;

        powerValue = 50.0f; // 吹力的值再加50
        powerValueIncrement = (120.0f / 100.0f) * 2.0f;
        powerBarAmount = 0.0f;
        powerBarAmountIncrement = (120.0f / 100.0f) * 0.01f;
        powerValueImage = powerBar.GetComponent<Image>();

        scoreNumber = 0;

        distanceNumber = 0.0f;
        distanceNumberAddOne = 3.0f; // x轴的3个单位为路程的1个单位
        distanceNumberMinusOne = -3.0f;
    }

    private void FixedUpdate()
    {
        CheckOnGround();

        if (onInhaleState) // 吸气
        {
            if (!animator.GetBool("inhaling"))
            {
                animator.SetBool("idling", false);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", true);
                animator.SetBool("blowing", false);
                animator.SetBool("moving", false);

                canJump = false;
                speed = 0.0f;
            }

            IncreasePower();

            DisableJumpButton();
        }
        else if (onBlowState) //吹气
        {
            if (!animator.GetBool("blowing"))
            {
                animator.SetBool("idling", false);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", true);
                animator.SetBool("moving", false);

                canJump = false;
                speed = 0.0f;
            }

            DisableBlowButton();
            DisableJumpButton();

            animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
            // 判断动画是否结束... (normalizedTime是一个0~1的浮点数，0代表动画的开头，1表示动画的结尾)
            if (animatorInfo.IsName("PlayerBlow") && animatorInfo.normalizedTime >= 1.0f)
            {
                onBlowState = false;

                if (!canMove)
                {
                    tutorWordsController.gameObject.SetActive(true);
                    nextWordsSound.Play();

                    if (tutorWordsController.wordsNumber == 10)
                    {
                        EnableBlowButton();
                    }
                }
            }
        }
        else if (!onGround) // 跳跃
        {
            if (!animator.GetBool("jumping"))
            {
                animator.SetBool("idling", false);
                animator.SetBool("jumping", true);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
                animator.SetBool("moving", false);

                canJump = false;
            }

            if (canMove)
            {
                GetAccX();
                DisableBlowButton();
            }
        }
        else if (onGround && speed == 0.0f) // 原地
        {
            if (!animator.GetBool("idling"))
            {
                animator.SetBool("idling", true);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
                animator.SetBool("moving", false);

                canJump = true;
            }

            if (canMove)
            {
                GetAccX();
                if (free)
                {
                    EnableJumpButton();
                    EnableBlowButton();
                }
            }
        }
        else if (onGround && speed != 0.0f) // 移动
        {
            if (!animator.GetBool("moving"))
            {
                animator.SetBool("idling", false);
                animator.SetBool("jumping", false);
                animator.SetBool("inhaling", false);
                animator.SetBool("blowing", false);
                animator.SetBool("moving", true);

                canJump = true;
            }

            if (canMove)
            {
                GetAccX();
                if (free)
                {
                    EnableJumpButton();
                    EnableBlowButton();
                }
            }
        }

        Move();

        CheckDistance();
    }

    private void GetAccX()
    {
        speed = Input.acceleration.x * 30.0f;
        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

        #region 测试用，键盘控制速度

        speed += Input.GetAxis("Horizontal") * 4.0f;

        #endregion 测试用，键盘控制速度
    }

    private void Move()
    {
        body.velocity = new Vector2(speed, body.velocity.y);
    }

    public void Jump()
    {
        if (onGround && canJump)
        {
            body.AddForce(new Vector2(0.0f, jumpForce));

            jumpSound.Play();

            if (tutorWordsController.wordsNumber == 5)
            {
                tutorWordsController.wordsNumber = 6;
                tutorWordsController.OnWordsClick();
            }
        }
    }

    public void Inhale()
    {
        if (!onBlowState && onGround)
        {
            onInhaleState = true;
        }
    }

    public void Blow()
    {
        if (onInhaleState)
        {
            onInhaleState = false;
            onBlowState = true;

            blowSound.Play();

            air.SetActive(true);

            if (tutorWordsController.wordsNumber == 7)
            {
                tutorWordsController.wordsNumber = 8;
                tutorWordsController.OnWordsClick();
                tutorWordsController.gameObject.SetActive(false);
            }
            else if (tutorWordsController.wordsNumber == 10)
            {
                tutorWordsController.gameObject.SetActive(false);

                if (powerBarAmount >= 1.0f)
                {
                    tutorWordsController.wordsNumber = 11;
                    tutorWordsController.OnWordsClick();
                }
            }
        }
    }

    private void CheckDistance()
    {
        distanceNumber = tutorPlayerTransform.position.x;

        if (distanceNumber >= distanceNumberAddOne)
        {
            int n = (int)(distanceNumberAddOne / 3.0f);
            distanceText.text = "路程：" + n.ToString();
            distanceNumberAddOne += 3.0f;
            distanceNumberMinusOne += 3.0f;

            UpdateScore(1);
        }
        else if (distanceNumber <= distanceNumberMinusOne)
        {
            int n = (int)(distanceNumberMinusOne / 3.0f);
            distanceText.text = "路程：" + n.ToString();
            distanceNumberAddOne -= 3.0f;
            distanceNumberMinusOne -= 3.0f;

            UpdateScore(-1);
        }
    }

    private void CheckOnGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        Debug.DrawLine(groundCheck.position, groundCheck.position + new Vector3(groundCheckRadius, 0.0f, 0.0f), Color.red);
        Debug.DrawLine(groundCheck.position, groundCheck.position - new Vector3(0.0f, groundCheckRadius, 0.0f), Color.red);
    }

    public void UpdateScore(int n)
    {
        scoreNumber += n;

        scoreText.text = "分数：" + scoreNumber.ToString();
    }

    public void IncreasePower()
    {
        if (powerBarAmount < 1.0f)
        {
            powerBarAmount += powerBarAmountIncrement;
            powerValueImage.fillAmount = powerBarAmount;
            powerValue += powerValueIncrement;
        }
    }

    public void ResetPower()
    {
        powerBarAmount = 0.0f;
        powerValueImage.fillAmount = powerBarAmount;
        powerValue = 50.0f;
        air.transform.localScale = Vector3.one;
    }

    private void DisappearAir()
    {
        air.SetActive(false);
        air.GetComponent<CapsuleCollider2D>().enabled = true;

        ResetPower();
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

    public void OnBlowButtonPress()
    {
        Inhale();
    }

    public void OnBlowButtonRelease()
    {
        Blow();
        //planeController.GetPower(powerValue);
        air.transform.localScale = new Vector3(air.transform.localScale.x * powerBarAmount + 0.8f,
                                               air.transform.localScale.y * powerBarAmount + 0.8f,
                                               1.0f);
    }

    public void OnJumpButtonPress()
    {
        Jump();
    }

    public void OnJumpButtonRelease()
    {
        DisableJumpButton();
    }
}