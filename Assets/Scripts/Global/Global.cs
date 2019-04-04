using UnityEngine;
using System;

public class Global : MonoBehaviour
{
    public static bool SoundEnabled = true;
    public static bool AdsInitialized = false; // 用于判断Yodo1AdsController（切换场景不销毁）是否已实例化，避免重复实例化
    public static AudioSource AchievementSound;

    public static string PackageName;

    public static int TodayLoginTimes;

    private void Awake()
    {
        AchievementSound = GameObject.Find("AchievementSound").GetComponent<AudioSource>();

        PackageName = Application.identifier; // 获得包名

        TodayLoginTimes = GetTodayLoginTimes();
    }

    public int GetTodayLoginTimes()
    {
        int todayLoginTimes = 1;
        string currentDate = DateTime.Now.ToString("yyyy:MM:dd"); // 当前日期：年月日
        
        if (!PlayerPrefs.HasKey("PreviousLoginDate")) // 如果是安装游戏后的第一次启动
        {
            PlayerPrefs.SetString("PreviousLoginDate", currentDate);
            PlayerPrefs.SetInt("TodayLoginTimes", todayLoginTimes);

        }
        else // 如果并不是安装游戏后的第一次启动
        {
            if (PlayerPrefs.GetString("PreviousLoginDate") == currentDate) // 如果上一次的启动日期等于当前日期（没跨日）
            {
                todayLoginTimes = PlayerPrefs.GetInt("TodayLoginTimes"); // 获得本日启动的次数（不包括本次）
                // 本日启动次数加1，并保存
                todayLoginTimes += 1;
                PlayerPrefs.SetInt("TodayLoginTimes", todayLoginTimes);
            }
            else // 如果上一次的启动日期不等于当前日期（跨日）
            {
                PlayerPrefs.SetString("PreviousLoginDate", currentDate); // 将当日存入上一次的启动日期
                PlayerPrefs.SetInt("TodayLoginTimes", todayLoginTimes); // todayLoginTimes == 1，本日第一次启动
            }
        }

        return todayLoginTimes;
    }

}