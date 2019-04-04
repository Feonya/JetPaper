using System.Collections;
using UnityEngine;

public class CatsController : MonoBehaviour
{
    private GameObject player;
    protected Transform playerTransform;
    public GameObject plane;
    private PlaneController planeController;

    private Animator animator;
    private Rigidbody2D body;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    protected Transform catTransform;
    private BoxCollider2D catCollider;
    public AudioSource callMamaSound;
    public AudioSource mercySound;
    public GameObject callMamaText;

    public bool captured;
    private bool mercy;

    public AudioSource mainMusic;

    public Sprite planeImage;

    protected void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.GetComponent<Transform>();
        planeController = plane.GetComponent<PlaneController>();

        animator = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        catTransform = gameObject.GetComponent<Transform>();
        catCollider = gameObject.GetComponent<BoxCollider2D>();
        callMamaText.SetActive(false);
        captured = false;
        mercy = false;
    }

    private void FixedUpdate()
    {
        if (mercy)
        {
            return;
        }
        else if (!captured)
        {
            CheckPlayerInRange();
        }
        // 如果被玩家捕获
        else
        {
            FollowPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 如果前面已经有猫了，先放走那只猫
            if (planeController.hasACat)
            {
                planeController.ReleaseACat();
            }

            captured = true;
            callMamaSound.Play();
            spriteRenderer.flipX = false;
            catCollider.enabled = false; // 关闭碰撞
            StartCoroutine(ShowCallMamaText());

            planeController.CaptureACat();
        }
    }

    // 跟随玩家
    protected virtual void FollowPlayer()
    {
        catTransform.position = new Vector3(playerTransform.position.x - 1.0f, catTransform.position.y, 0.0f);
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (catTransform.position.x - playerTransform.position.x < -10.0f) // 如果玩家在后方10个单位之后
        {
            Destroy(gameObject);
        }
        else if (catTransform.position.x - playerTransform.position.x < 10.0f) // 如果玩家进入前方10个单位
        {
            body.velocity = new Vector3(-1.0f, 0.0f, 0.0f);
        }
    }

    // 救赎函数，用于在其他类中调用
    public void CallDoMercy()
    {
        if (!mercy)
        {
            StartCoroutine(DoMercy());
        }
    }

    // 救赎
    private IEnumerator DoMercy()
    {
        mercy = true;
        captured = false;

        mercySound.Play();
        mainMusic.Stop();

        if (!animator.GetBool("mercy"))
        {
            animator.SetBool("mercy", true);
        }

        body.velocity = new Vector3(0.0f, 0.3f, 0.0f);

        yield return new WaitForSeconds(8.0f);

        // 飞机的复活变形
        if (!plane.activeSelf)
        {
            plane.SetActive(true);
        }
        planeController.ReviveWithCat(planeImage, catTransform.position);

        mainMusic.Play();

        Destroy(gameObject);
    }

    // 显示叫妈妈文字3秒
    private IEnumerator ShowCallMamaText()
    {
        callMamaText.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        Destroy(callMamaText);
    }
}