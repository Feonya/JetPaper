using UnityEngine;

public class Yodo1AdsController : MonoBehaviour
{
    //private GameObject replayButton;
    //private GameObject showAdsFailed;
    //private GameObject rebirthButton;
    //private GameObject adsLoading;

    //private GameObject exitOrQuitButtons;
    //private GameObject didNotPlayButton;

    //public bool adClicked;

    private void Awake()
    {
        if (Global.PackageName != "com.tykj.jetpaper.tt" && Global.PackageName != "com.tykj.jetpaper.android")
        {
            //Destroy(GameObject.Find("Yodo1Ads"));
            Destroy(gameObject);
        }
        else
        {
            if (!Global.AdsInitialized)
            {
                DontDestroyOnLoad(gameObject);
                InitAds();

                Global.AdsInitialized = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void InitAds()
    {
        Yodo1U3dAds.InitWithAppKey(Yodo1U3dAdsSettings.appKey);

        // 视频广告的回调设置
        Yodo1U3dSDK.setRewardVideoDelegate((bool finished, string error) =>
        {
            if (finished)
            {
                //Debug.Log("广告观看播放完奖励");
                GemController.Add3Gem();
                GemController.Showed = true;

                TalkingDataController.WatchedAds();
            }
            else
            {
                
            }
        });
    }

#if UNITY_ANDROID

    private void OnApplicationPause(bool isPaused)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!isPaused)
            {
                Yodo1U3dInitForAndroid.onResume();
            }
            else
            {
                Yodo1U3dInitForAndroid.onPause();
            }
        }
    }

    private void OnDestroy()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Yodo1U3dInitForAndroid.onDestroy();
        }
    }

#endif
}