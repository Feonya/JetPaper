using System.Collections;
using UnityEngine;

public class ShoeContorller : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;
    private Animator playerAnimator;

    public GameObject almostWin;

    public AudioSource deadSound;
    public AudioSource hitSound;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        playerAnimator = player.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            deadSound.Play();
            hitSound.Play();
            playerAnimator.SetBool("isDead", true);

            StartCoroutine(ShowAlmostWin());
        }
    }

    private IEnumerator ShowAlmostWin()
    {
        yield return new WaitForSeconds(2.0f);

        almostWin.SetActive(true);
    }
}