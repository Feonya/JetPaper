using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebController : MonoBehaviour
{
    private PlayerController playerController;

    public PlaneController planeController;

    private void Start()
    {
        playerController = PlayerChooser.ChoosePlayer().GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plane"))
        {
            planeController.ForceIdle();
            planeController.GetComponent<Transform>().position = transform.position;

            playerController.Dead();
        }
    }
}
