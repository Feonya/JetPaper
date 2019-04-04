using UnityEngine;

public class SwarmsController : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;

    private Transform swarmsTransform;
    public AudioSource swarmsSound;

    private bool alive;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.transform;

        swarmsTransform = transform;

        alive = false;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();

        if (alive)
        {
            swarmsTransform.position -= new Vector3(0.1f, 0.0f, 0.0f);

            if (!swarmsSound.isPlaying)
            {
                swarmsSound.Play();
            }
        }
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (swarmsTransform.position.x - playerTransform.position.x < -20.0f) // 如果玩家在后方20个单位之后
        {
            Destroy(gameObject);
        }
        else if (swarmsTransform.position.x - playerTransform.position.x < 10.0f) // 如果玩家进入前方10个单位
        {
            alive = true;
        }
    }
}