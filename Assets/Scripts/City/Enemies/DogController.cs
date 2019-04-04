using UnityEngine;

public class DogController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    public AudioSource dogSound;
    private Animator animator;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();

        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false; // 初始化停止动画播放
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

    private void CheckPlayerInRange() // 检测玩家相对位置
    {
        if (transform.position.x - player.transform.position.x < 1.5f) // 如果玩家进入前方1.5个单位
        {
            if (!dogSound.isPlaying)
            {
                dogSound.Play();
            }
            animator.enabled = true;
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}