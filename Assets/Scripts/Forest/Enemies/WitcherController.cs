using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitcherController : MonoBehaviour
{
    public GemInGameController gemInGameController;

    public AudioSource witcherSound;

    private GameObject player;
    private PlayerController playerController;
    private Transform playerTransform;

    private Transform witcherTransform;

    private float originalPositionX;
    private float targetPositionX;

    private bool colliding;
    private bool collided;

    private bool alive;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        playerTransform = player.transform;

        witcherTransform = transform;

        originalPositionX = witcherTransform.position.x;

        colliding = false;
        collided = false;

        alive = false;

        GetRandomTargetPositionX();
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();

        if (alive)
        {
            if (witcherTransform.position.x != targetPositionX)
            {
                witcherTransform.position = Vector2.MoveTowards(witcherTransform.position, new Vector2(targetPositionX, witcherTransform.position.y), Time.fixedDeltaTime);
            }
            else
            {
                GetRandomTargetPositionX();
            }

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
                    playerController.canMove = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            if (!playerController.onDeadState)
            {
                witcherSound.Play();
                playerController.canMove = false;
                playerController.body.velocity = Vector2.zero;

                var leftOrRight = (player.transform.position.x - transform.position.x);
                if (leftOrRight <= 0)
                {
                    playerController.body.velocity = new Vector2(-3.0f, 10.0f);
                }
                else
                {
                    playerController.body.velocity = new Vector2(3.0f, 10.0f);
                }

                colliding = true;

                if (!playerController.onDeadState && GemController.Number > 0)
                {
                    GemController.Number -= 1;
                    GemController.Showed = false;
                    PlayerPrefs.SetInt("GemNumber", GemController.Number);

                    gemInGameController.UpdateNumberText();
                }
            }
        }
    }

    private void GetRandomTargetPositionX()
    {
        targetPositionX = Random.Range(-4.5f, 4.5f) + originalPositionX;

        if (witcherTransform.position.x > targetPositionX)
        {
            witcherTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (witcherTransform.position.x < targetPositionX)
        {
            witcherTransform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (witcherTransform.position.x - playerTransform.position.x < -10.0f) // 如果玩家在后方10个单位之后
        {
            alive = false;
        }
        else if (witcherTransform.position.x - playerTransform.position.x < 6.0f) // 如果玩家进入前方6个单位
        {
            alive = true;
        }
    }
}
