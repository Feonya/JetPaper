using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    public AudioSource appleSound;

    private Animator animator;

    private bool alive;

    private void Start()
    {
        animator = GetComponent<Animator>();

        alive = false;
    }

    private void FixedUpdate()
    {
        if (ApplesController.alive)
        {
            if (!alive)
            {
                StartCoroutine(RandomDrop(1.0f, 10.0f));
                alive = true;
            }
        }
        else
        {
            if (alive)
            {
                alive = false;
            }
        }
    }

    IEnumerator RandomDrop(float min, float max)
    {
        yield return new WaitForSeconds(Random.Range(min, max));

        Drop();
    }

    private void Drop()
    {
        if (!animator.enabled)
        {
            animator.enabled = true;
        }
    }

    private void Sound()
    {
        appleSound.Play();
    }

    private void Reset()
    {
        if (animator.enabled)
        {
            animator.enabled = false;
        }
        if (alive)
        {
            StartCoroutine(RandomDrop(4.0f, 10.0f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerChooser.ChoosePlayer().GetComponent<PlayerController>().Dead();
        }
    }
}
