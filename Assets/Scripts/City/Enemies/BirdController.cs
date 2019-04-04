using UnityEngine;

public class BirdController : MonoBehaviour
{
    public AudioSource birdSound;
    public GameObject plane;
    private GameObject player;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plane"))
        {
            plane.GetComponent<Rigidbody2D>().velocity += new Vector2(0.0f, -3.0f);
        }
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (transform.position.x - player.transform.position.x < -10.0f) // 如果玩家在后方10个单位之后
        {
            Destroy(gameObject);
        }
        else if (transform.position.x - player.transform.position.x < 10.0f) // 如果玩家进入前方10个单位
        {
            if (!birdSound.isPlaying)
            {
                birdSound.Play();
            }
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-3.0f, Mathf.Sin(gameObject.transform.position.x * 2.0f) * 8.0f, 0.0f);
        }
    }
}