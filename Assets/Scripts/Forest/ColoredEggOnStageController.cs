using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredEggOnStageController : MonoBehaviour
{
    private bool beFound;

    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        beFound = AchievementsAndHighscoresController.achievementListRecorder.cardsName.Contains("ColoredEggCard");

        if (beFound)
        {
            Destroy(gameObject);
        }
        else
        {
            player = PlayerChooser.ChoosePlayer();
            playerController = player.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AchievementsAndHighscoresController.achievementListRecorder.EnableCard("ColoredEggCard");
            Destroy(gameObject);
        }
    }
}
