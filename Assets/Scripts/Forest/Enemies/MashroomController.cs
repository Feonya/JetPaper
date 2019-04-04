using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashroomController : MonoBehaviour
{
    public AudioSource gasSound;

    private Animator animator;
    private Transform mashroomTransform;

    private GameObject player;
    private Transform playerTransform;

    private bool alive;

    private void Start()
    {
        animator = GetComponent<Animator>();
        mashroomTransform = transform;

        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.transform;

        alive = false;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();

        if (alive)
        {
            if (!animator.enabled)
            {
                animator.enabled = true;
            }
        }
        else
        {
            if (animator.enabled)
            {
                animator.enabled = false;
            }
        }
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (mashroomTransform.position.x - playerTransform.position.x < -10.0f) // 如果玩家在后方10个单位之后
        {
            alive = false;
        }
        else if (mashroomTransform.position.x - playerTransform.position.x < 6.0f) // 如果玩家进入前方6个单位
        {
            alive = true;
        }
    }

    public void StartSound()
    {
        if (!gasSound.isPlaying)
        {
            gasSound.Play();
        }
    }
}
