using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenGemsPerDay : MonoBehaviour
{
    private void Start()
    {
        if (AchievementsAndHighscoresController.achievementListRecorder.cardsName.Contains("TenGemsPerDayCard"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AchievementsAndHighscoresController.achievementListRecorder.EnableCard("TenGemsPerDayCard");
            Destroy(gameObject);
        }
    }
}
