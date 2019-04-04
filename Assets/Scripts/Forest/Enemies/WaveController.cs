using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public AudioSource hitSound;

    private GameObject player;
    private PlayerController playerController;
    private Transform playerTransform;

    private Animator animator;
    private AnimatorStateInfo animatorInfo;

    private bool colliding;
    private bool collided;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        playerTransform = player.transform;

        animator = transform.parent.GetComponent<Animator>();

        colliding = false;
        collided = false;
    }

    private void FixedUpdate()
    {
        if (colliding)
        {
            if (!playerController.onGround)
            {
                colliding = false;
                collided = true;
            }
        }
        else if (collided)
        {
            if (playerController.onGround)
            {
                collided = false;
                playerController.body.velocity = Vector2.zero;
                playerController.canMove = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hitSound.Play();
            playerController.canMove = false;
            playerController.body.velocity = Vector2.zero;
            playerController.body.velocity = new Vector2(-7.0f, 10.0f);
            colliding = true;
        }
    }
}
