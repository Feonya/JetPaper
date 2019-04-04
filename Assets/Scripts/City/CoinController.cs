using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioSource coinSound;

    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Plane"))
        {
            coinSound.Play();

            // 加分
            playerController.UpdateScore(1);

            Destroy(gameObject);
        }
    }
}