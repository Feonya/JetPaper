using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasController : MonoBehaviour
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
            playerController.EatShit();
        }
    }
}
