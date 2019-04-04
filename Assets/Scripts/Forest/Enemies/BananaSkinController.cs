using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSkinController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    public AudioSource tripSound;

    private void Start()
    {
        gameObject.SetActive(false);

        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.transform.SetAsFirstSibling(); // 设为视图列表父物体下的第一个物体
            gameObject.SetActive(false);
            if (!playerController.onTrippedState)
            {
                tripSound.Play();
            }
            playerController.Trip();
        }
    }
}
