using System.Collections;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;

    private Transform handTransform;
    private Animator animator;

    public GameObject handBombs;
    private GameObject bomb;
    private Transform bombTransform;
    private Rigidbody2D bombBody;
    private SpriteRenderer bombSpriteRenderer;
    private HandBombController bombController;

    private bool alive;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.transform;

        handTransform = transform;
        animator = GetComponent<Animator>();
        animator.enabled = false;

        alive = false;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();

        if (alive)
        {
            if (!animator.enabled)
            {
                animator.enabled = true;
            }
        }
        else
        {
            if (animator.enabled)
            {
                animator.enabled = false;
            }
        }
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (handTransform.position.x - playerTransform.position.x < -5.5f) // 如果玩家在后方5.5个单位之后
        {
            if (alive)
            {
                alive = false;
            }
        }
        else if (handTransform.position.x - playerTransform.position.x < 5.5f) // 如果玩家进入前方5.5个单位
        {
            if (!alive)
            {
                alive = true;
            }
        }
    }

    private void CallThrow()
    {
        animator.Play("Empty");
        StartCoroutine(Throw());
    }

    private IEnumerator Throw()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 3.0f));
        animator.Play("HandThrow");
    }

    private void BombFly()
    {
        // 获取第一个bomb并激活
        bombBody = handBombs.GetComponentInChildren<Rigidbody2D>(true);
        bomb = bombBody.gameObject;
        bombTransform = bomb.GetComponent<Transform>();
        bombSpriteRenderer = bomb.GetComponent<SpriteRenderer>();
        bombController = bomb.GetComponent<HandBombController>();

        bombSpriteRenderer.enabled = true;
        bomb.SetActive(true);

        bombBody.AddForce(new Vector2(Random.Range(-200.0f, 200.0f), Random.Range(200.0f, 400.0f))); // 随机力
        bombBody.AddTorque(Random.Range(-45.0f, 45.0f));

        bombTransform.SetAsLastSibling(); // 置后

        bombController.CallWaitExplosion();
    }
}