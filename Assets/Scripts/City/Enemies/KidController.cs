using UnityEngine;

public class KidController : MonoBehaviour
{
    protected GameObject player;
    private PlayerController playerController;
    private Transform playerTransform;

    protected Transform kidTransform;
    protected Animator animator;

    private bool alive;
    private bool onMove;
    protected bool onShit;

    // 小孩左右移动的界限
    private float rightLimit;

    private float leftLimit;

    private Vector2 targetPosition; // 移动目标位置

    private float speed;

    public GameObject kidShits;

    //private KidShitController kidShitController;
    protected GameObject shit;

    protected Transform shitTransform;

    public AudioSource shitSound;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        playerTransform = player.GetComponent<Transform>();

        kidTransform = gameObject.GetComponent<Transform>();
        animator = gameObject.GetComponent<Animator>();

        alive = false;
        onMove = false;
        onShit = true;

        rightLimit = kidTransform.position.x;
        leftLimit = rightLimit - 10.0f;

        targetPosition.x = Random.Range(leftLimit, rightLimit);
        targetPosition.y = kidTransform.position.y;

        speed = 2.0f;

        shitTransform = kidShits.transform.GetChild(0);
        shit = shitTransform.gameObject;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();

        if (alive)
        {
            Move();
            Shit();
        }
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (kidTransform.position.x - playerTransform.position.x < -10.0f) // 如果玩家在后方10个单位之后
        {
            alive = false;
        }
        else if (kidTransform.position.x - playerTransform.position.x < 10.0f) // 如果玩家进入前方10个单位
        {
            alive = true;
        }
    }

    // 计算随机移动目标位置
    private void CheckTargetX()
    {
        targetPosition.x = Random.Range(leftLimit, rightLimit);
    }

    // 移动
    private void Move()
    {
        if (onMove)
        {
            kidTransform.position = Vector2.MoveTowards(kidTransform.position, targetPosition, speed * Time.fixedDeltaTime);

            // 如果移动到了目标位置
            if (kidTransform.position.x == targetPosition.x)
            {
                onMove = false;
                onShit = true;
            }
        }
    }

    // 拉屎
    protected virtual void Shit()
    {
        if (onShit)
        {
            if (!animator.GetBool("shitting"))
            {
                animator.SetBool("shitting", true);
                animator.SetBool("moving", false);

                shit.SetActive(true);
                shitTransform.position = kidTransform.position - new Vector3(0.16f, 0.24f, 0.0f);
                shitTransform.SetAsLastSibling(); // 拉出来的屎放在父物体下的最后

                shitSound.Play();
            }
        }
    }

    // 拉完屎
    private void FinishShit()
    {
        CheckTargetX();
        onShit = false;
        onMove = true;

        // 获得新的父物体下第一泡屎
        shitTransform = kidShits.transform.GetChild(0);
        shit = shitTransform.gameObject;

        if (!animator.GetBool("moving"))
        {
            animator.SetBool("shitting", false);
            animator.SetBool("moving", true);
        }
    }
}