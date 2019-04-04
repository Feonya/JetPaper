using UnityEngine;

public class StickController : MonoBehaviour
{
    private PlayerController playerController;

    public AudioSource tripSound;

    private void Start()
    {
        playerController = PlayerChooser.ChoosePlayer().GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.Trip();
            tripSound.Play();
        }
    }
}