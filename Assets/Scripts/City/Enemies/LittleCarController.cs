using System.Collections;
using UnityEngine;

public class LittleCarController : MonoBehaviour
{
    public Transform colliderCheck;
    private Vector2 colliderCheckSize;
    public LayerMask whatIsCollider;
    private bool collided;

    private GameObject player;
    private PlayerController playerController;
    private Transform playerTransform;
    public AudioSource littleCarSound;
    public AudioSource collideSound;

    private Transform carTransform;
    private Rigidbody2D body;

    private bool firstCollide; // 是否是首次撞击

    public GameObject shock;

    private void Start()
    {
        colliderCheckSize = new Vector2(0.1f, 0.2f);
        collided = false;

        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        playerTransform = player.transform;

        body = gameObject.GetComponent<Rigidbody2D>();
        carTransform = gameObject.transform;

        firstCollide = true;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();
        CheckCollide();
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (carTransform.position.x - playerTransform.position.x < -10.0f) // 如果玩家在后方10个单位之后
        {
            Destroy(gameObject);
        }
        else if (carTransform.position.x - playerTransform.position.x < 10.0f) // 如果玩家进入前方10个单位
        {
            if (carTransform.position.x - playerTransform.position.x > -0.5f)
            {
                if (!littleCarSound.isPlaying)
                {
                    littleCarSound.Play();
                }
            }
            body.velocity = new Vector2(-2.0f, 0.0f);
        }
    }

    private void CheckCollide()
    {
        collided = Physics2D.OverlapBox(colliderCheck.position, colliderCheckSize, 0.0f, whatIsCollider);

        Debug.DrawLine(colliderCheck.position, colliderCheck.position - new Vector3(colliderCheckSize.x, 0.0f, 0.0f), Color.red);
        Debug.DrawLine(colliderCheck.position, colliderCheck.position + new Vector3(0.0f, colliderCheckSize.y, 0.0f), Color.red);

        if (collided && firstCollide)
        {
            StartCoroutine(Shock());
            playerController.Dead();
            firstCollide = false;
            collideSound.Play();
        }

        if (!collided)
        {
            firstCollide = true;
        }
    }

    private IEnumerator Shock()
    {
        shock.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        shock.SetActive(false);
    }
}