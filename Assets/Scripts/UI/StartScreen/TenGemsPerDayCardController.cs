using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TenGemsPerDayCardController : MonoBehaviour
{
    private void Start()
    {
        if (Global.TodayLoginTimes == 1)
        {
            if (AchievementsAndHighscoresController.achievementListRecorder.cardsName.Contains("TenGemsPerDayCard"))
            {
                GetComponent<Button>().interactable = true;
                AchievementsAndHighscoresController.achievementListRecorder.cardsClickable[AchievementsAndHighscoresController.achievementListRecorder.cardsName.IndexOf("TenGemsPerDayCard")] = true;
                AchievementsAndHighscoresController.achievementListRecorder.newCardNumber += 1;

                string achievementListJson = JsonUtility.ToJson(AchievementsAndHighscoresController.achievementListRecorder);
                PlayerPrefs.SetString("AchievementListRecorder", achievementListJson);
            }
        }
    }
}
