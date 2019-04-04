using UnityEngine;

public class SwordController : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;
    private PlayerController playerController;

    private Transform swordTransform;
    public AudioSource swordSound;
    private Animator swordAnimator;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();

        swordTransform = gameObject.GetComponent<Transform>();
        swordAnimator = gameObject.GetComponent<Animator>();
        swordAnimator.enabled = false; // 关闭动画
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.Dead();
        }
    }

    public void Stab()
    {
        swordSound.Play();
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (swordTransform.position.x - playerTransform.position.x < -4.0f) // 如果玩家在后方4个单位之后
        {
            swordAnimator.enabled = false;
        }
        else if (swordTransform.position.x - playerTransform.position.x < 4.0f) // 如果玩家进入前方4个单位
        {
            swordAnimator.enabled = true;
        }
    }
}