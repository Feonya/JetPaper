using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwormController : MonoBehaviour
{
    public AudioSource sandwormSound;

    private GameObject player;
    private PlayerController playerController;
    private Transform playerTransform;

    private Animator animator;
    private Transform sandwormTransform;

    private bool colliding;
    private bool collided;

    private bool alive;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        playerTransform = player.transform;

        animator = GetComponent<Animator>();
        sandwormTransform = transform;

        colliding = false;
        collided = false;

        alive = false;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();

        if (alive)
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
    }

    private void Sound()
    {
        sandwormSound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.canMove = false;
            playerController.body.velocity = Vector2.zero;

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

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (sandwormTransform.position.x - playerTransform.position.x < -2.5f) // 如果玩家在后方1.5个单位之后
        {
            if (alive)
            {
                alive = false;
                animator.enabled = false;
            }
        }
        else if (sandwormTransform.position.x - playerTransform.position.x < 1.5f) // 如果玩家进入前方1.5个单位
        {
            if (!alive)
            {
                alive = true;
                animator.enabled = true;
            }
        }
    }
}
