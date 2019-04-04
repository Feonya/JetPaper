using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private Collider2D playerCollider;

    private bool colliding;
    private bool collided;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        playerCollider = player.GetComponent<Collider2D>();

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
                playerController.Dead();
            }
        }
        else if (collided)
        {
            if (playerController.onGround)
            {
                collided = false;
                playerController.body.velocity = new Vector2(0.0f, playerController.body.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.canMove = false;
            playerController.body.velocity = Vector2.zero;
            //playerController.Dead();

            var leftOrRight = (player.transform.position.x - transform.position.x);
            if (leftOrRight <= 0)
            {
                playerController.body.velocity = new Vector2(-2.0f, 10.0f);
            }
            else
            {
                playerController.body.velocity = new Vector2(2.0f, 10.0f);
            }

            colliding = true;
        }
    }
}
