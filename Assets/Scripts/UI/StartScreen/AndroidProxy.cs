using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidProxy : MonoBehaviour {

    [HideInInspector]
    public static GameObject instance; //单例
    [HideInInspector]
    public static AndroidJavaObject joM4399;
    [HideInInspector]
    public static AndroidJavaObject joMeizu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);

            if (Global.PackageName == "com.tykj.jetpaper.m4399")
            {
                AndroidJavaClass jcM4399 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                joM4399 = jcM4399.GetStatic<AndroidJavaObject>("currentActivity");
            }
            else if (Global.PackageName == "com.tykj.jetpaper.mz")
            {
                AndroidJavaClass jcMeizu = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                joMeizu = jcMeizu.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 实现GemController.cs中的Add3Gem()
    void Add3Gem(string str)
    {
        GemController.Number += 3;
        PlayerPrefs.SetInt("GemNumber", GemController.Number);
        GemController.Showed = true;
    }

    // 实现TalkingDataController.cs中的WatchedAds()
    void WatchedAds(string str)
    {
        TalkingDataController.WatchedAds();
    }
}
