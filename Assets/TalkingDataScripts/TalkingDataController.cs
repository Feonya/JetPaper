using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingDataController : MonoBehaviour
{
    private static GameObject TheFirstGameObject;

    private static Dictionary<string, object> dic = new Dictionary<string, object>();

    public static bool GamePlayed;
    public static int DeadTimes;
    public static bool DistanceGreaterThan30;

    private void Start()
    {
        if (TheFirstGameObject == null)
        {
            TheFirstGameObject = gameObject;
            DontDestroyOnLoad(gameObject);

            dic.Add("IntValue", 1);

            GamePlayed = false; // 在进入每个管卡后设置为false
            DeadTimes = 0;
            DistanceGreaterThan30 = false;

            if (Global.PackageName == "com.tykj.jetpaper.tt")
            {
                TalkingDataGA.OnStart("9FD9F8311C634D36899E92AD8C288220", "TapTap");
            }
            else if (Global.PackageName == "com.tykj.jetpaper.vivo")
            {
                TalkingDataGA.OnStart("9FD9F8311C634D36899E92AD8C288220", "Vivo");
            }
            else if (Global.PackageName == "com.tykj.jetpaper.mz")
            {
                TalkingDataGA.OnStart("9FD9F8311C634D36899E92AD8C288220", "Meizu");
            }
            else if (Global.PackageName == "com.tykj.jetpaper.m4399")
            {
                TalkingDataGA.OnStart("9FD9F8311C634D36899E92AD8C288220", "M4399");
            }
            else if (Global.PackageName == "com.tykj.jetpaper.nearme.gamecenter")
            {
                TalkingDataGA.OnStart("9FD9F8311C634D36899E92AD8C288220", "Oppo");
            }
            else
            {
                TalkingDataGA.OnStart("9FD9F8311C634D36899E92AD8C288220", "Others");
            }

            TDGAAccount.SetAccount(TalkingDataGA.GetDeviceId());
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void CheckDeadTimes()
    {
        if (DeadTimes >= 3)
        {
            TalkingDataGA.OnEvent("退出时死亡次数大于等于3的次数", dic);
        }
    } 

    public void CheckDistance()
    {
        if (DistanceGreaterThan30)
        {
            TalkingDataGA.OnEvent("退出时路程曾大于等于30的次数", dic);
        }
    }

    public void CheckExitWithoutPlayed()
    {
        if (!GamePlayed)
        {
            TalkingDataGA.OnEvent("退出而未进入任意关卡次数", dic);
        }
    }

    public static void WatchedAds()
    {
        TalkingDataGA.OnEvent("广告完整观看次数", dic);
    }

    private void OnApplicationQuit() // 退出游戏调用
    {
        CheckExitWithoutPlayed();
        CheckDeadTimes();
        CheckDistance();

        TalkingDataGA.OnEnd();
    }
}
