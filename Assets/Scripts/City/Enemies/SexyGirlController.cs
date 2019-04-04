using UnityEngine;

public class SexyGirlController : MonoBehaviour
{
    public AudioSource kissSound;
    public GameObject kissHeart;
    protected Rigidbody2D kissHeartBody;
    protected Transform kissHeartTransform;
    protected Vector3 mouthPosition;

    private Transform sexyGirlTransform;
    private Animator sexyGirlAnimator;
    protected bool startKiss;

    private GameObject player;
    private Transform playerTransform;

    protected void Start()
    {
        kissHeartTransform = kissHeart.transform;
        kissHeartBody = kissHeart.GetComponent<Rigidbody2D>();
        mouthPosition = kissHeartTransform.position; // 嘴的位置

        startKiss = false;

        sexyGirlTransform = transform;

        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.transform;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (sexyGirlTransform.position.x - playerTransform.position.x < -5.5f) // 如果玩家在后方5.5个单位之后
        {
            if (startKiss)
            {
                startKiss = false;
            }
        }
        else if (sexyGirlTransform.position.x - playerTransform.position.x < 5.5f) // 如果玩家进入前方5.5个单位
        {
            if (!startKiss)
            {
                startKiss = true;
            }
        }
    }

    protected virtual void Kiss()
    {
        if (startKiss)
        {
            kissHeart.SetActive(true);
            kissHeartTransform.position = mouthPosition;
            kissSound.Play();

            Vector2 direction = kissHeartTransform.position - playerTransform.position;
            direction = -direction.normalized;

            kissHeartBody.velocity = direction * 2.0f;
        }
        else
        {
            if (kissHeart.activeSelf)
            {
                kissHeart.SetActive(false);
            }
        }
    }
}