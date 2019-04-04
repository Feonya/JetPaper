using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMouseController : MonoBehaviour
{
    public AudioSource flyMouseSound;

    private Transform playerTransform;

    public GameObject plane;
    private PlaneController planeController;
    private Transform planeTransform;

    private Transform flyMouseTransform;
    private Animator animator;

    private bool alive;
    private bool planeCaptured;

    private int treeNumber;

    private void Start()
    {
        playerTransform = PlayerChooser.ChoosePlayer().transform;

        planeController = plane.GetComponent<PlaneController>();
        planeTransform = plane.transform;

        flyMouseTransform = transform;
        animator = GetComponent<Animator>();

        alive = false;
        planeCaptured = false;

        treeNumber = 0;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();

        if (alive)
        {
            if (planeCaptured)
            {
                planeTransform.position = flyMouseTransform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plane"))
        {
            planeCaptured = true;
            planeController.DisablePhysicsSimulate();
        }
    }

    private void Sound()
    {
        flyMouseSound.Play();
    }

    private void RandomThrowPlane()
    {
        if (planeCaptured)
        {
            int rand = Random.Range(1, 7);
            treeNumber += 1;

            if (rand == treeNumber)
            {
                planeCaptured = false;
                planeController.EnablePhysicsSimulate();
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    private void ThrowPlane()
    {
        if (planeCaptured)
        {
            planeCaptured = false;
            planeController.EnablePhysicsSimulate();
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void Disable()
    {
        Destroy(gameObject);
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (flyMouseTransform.position.x - playerTransform.position.x < 10f) // 如果玩家进入前方10个单位
        {
            if (!alive)
            {
                alive = true;
                animator.enabled = true;
            }
        }
    }
}
