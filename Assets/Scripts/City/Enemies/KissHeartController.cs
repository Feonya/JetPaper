using UnityEngine;

public class KissHeartController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 被迷住
            playerController.Love();

            gameObject.SetActive(false);
        }
    }
}