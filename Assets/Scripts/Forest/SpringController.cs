using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    private GameObject player;

    public Sprite upspring;
    public Sprite spring;

    public AudioSource upspringSound;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                var contactNormal = collision.GetContact(i).normal;

                if (contactNormal.y == -1.0f)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 1300.0f));
                    GetComponent<SpriteRenderer>().sprite = upspring;
                    upspringSound.Play();
                    StartCoroutine(ResetSpring());
                }
            }
        }
    }

    IEnumerator ResetSpring()
    {
        yield return new WaitForSeconds(1.0f);

        GetComponent<SpriteRenderer>().sprite = spring;
    }
}
