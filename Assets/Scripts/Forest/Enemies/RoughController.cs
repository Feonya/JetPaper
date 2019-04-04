using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoughController : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = PlayerChooser.ChoosePlayer().GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.onRough = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.onRough = false;
        }
    }
}
