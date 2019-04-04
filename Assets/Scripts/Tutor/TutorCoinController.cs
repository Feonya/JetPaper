using UnityEngine;

public class TutorCoinController : MonoBehaviour
{
    public AudioSource coinSound;
    public TutorPlayerController tutorPlayerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Plane"))
        {
            coinSound.Play();

            // 加分
            tutorPlayerController.UpdateScore(1);

            Destroy(gameObject);
        }
    }
}