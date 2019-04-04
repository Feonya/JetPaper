using UnityEngine;

public class StartLineController : MonoBehaviour
{
    public TutorWordsController tutorWordsController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorWordsController.wordsNumber = 4;
            tutorWordsController.OnWordsClick();

            GetComponent<Collider2D>().enabled = false;
        }
    }
}