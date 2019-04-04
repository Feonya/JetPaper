using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAdsButtonController : MonoBehaviour
{
    public GameObject showAdsFailed;

    public void OnShowAdsButtonClick()
    {
        if (Global.PackageName == "com.tykj.jetpaper.tt" || Global.PackageName == "com.tykj.jetpaper.android")
        {
            if (Yodo1U3dAds.VideoIsReady())
            {
                Yodo1U3dAds.ShowVideo();
            }
            else
            {
                showAdsFailed.SetActive(true);
            }
        }
        else if (Global.PackageName == "com.tykj.jetpaper.m4399")
        {
            if (AndroidProxy.joM4399.Call<string>("CheckAds") == "Loaded")
            {
                AndroidProxy.joM4399.Call("ShowAds");
            }
            else
            {
                showAdsFailed.SetActive(true);
            }
        }
        else if (Global.PackageName == "com.tykj.jetpaper.mz")
        {
            if (AndroidProxy.joMeizu.Call<string>("CheckAds") == "Loaded")
            {
                AndroidProxy.joMeizu.Call("ShowAds");
            }
            else
            {
                showAdsFailed.SetActive(true);
            }
        }
        else
        {
            Debug.Log("尚未接入其他广告SDK");

            // ***************************************************模拟广告已观看
            GemController.Add3Gem();
            GemController.Showed = true;
            TalkingDataController.WatchedAds();
        }
    }
}
