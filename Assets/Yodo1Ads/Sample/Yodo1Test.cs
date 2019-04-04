using UnityEngine;

public class Yodo1Test : MonoBehaviour
{
    private bool isTimes;

    private void Start()
    {
        //SDK初始化
        Debug.Log("hyx-->yodo1 is init....");
        isTimes = true;

        Yodo1U3dAds.SetLogEnable(true);
        Yodo1U3dAds.InitWithAppKey(Yodo1U3dAdsSettings.appKey);

        Yodo1U3dSDK.setBannerdDelegate((Yodo1U3dConstants.AdEvent adEvent, string error) =>
        {
            Debug.Log("BannerdDelegate:" + adEvent + "\n" + error);
            switch (adEvent)
            {
                case Yodo1U3dConstants.AdEvent.AdEventClick:
                    Debug.Log("Banner点击广告");
                    break;

                case Yodo1U3dConstants.AdEvent.AdEventClose:
                    Debug.Log("Banner关闭广告");
                    break;

                case Yodo1U3dConstants.AdEvent.AdEventLoaded:
                    Debug.Log("Banner下载好了广告");
                    break;

                case Yodo1U3dConstants.AdEvent.AdEventLoadFail:
                    Debug.Log("Banner广告加载失败：" + error);
                    break;
            }
        });

        Yodo1U3dSDK.setInterstitialAdDelegate((Yodo1U3dConstants.AdEvent adEvent, string error) =>
        {
            Debug.Log("InterstitialAdDelegate:" + adEvent + "\n" + error);
            switch (adEvent)
            {
                case Yodo1U3dConstants.AdEvent.AdEventClick:
                    Debug.Log("Interstital点击广告");
                    break;

                case Yodo1U3dConstants.AdEvent.AdEventClose:
                    Debug.Log("Interstital关闭广告");
                    break;

                case Yodo1U3dConstants.AdEvent.AdEventLoaded:
                    Debug.Log("Interstital下载好了广告");
                    break;

                case Yodo1U3dConstants.AdEvent.AdEventLoadFail:
                    Debug.Log("Interstital广告加载失败：" + error);
                    break;

                case Yodo1U3dConstants.AdEvent.AdEventShowFail:
                    Debug.Log("Interstital广告展示失败：" + error);
                    break;
            }
        });

        Yodo1U3dSDK.setRewardVideoDelegate((bool finished, string error) =>
        {
            if (finished)
            {
                Debug.Log("广告观看播放完奖励");
            }
            else
            {
                Debug.Log("没有广告或是没有看完广告");
            }
        });
    }

    private void Update()
    {
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

    private void OnGUI()
    {
        int buttonHeight = Screen.height / 13;
        int buttonWidth = Screen.width / 2;
        int buttonSpace = buttonHeight / 2;
        int startHeight = buttonHeight / 2;
        if (GUI.Button(new Rect(Screen.width / 4, startHeight, buttonWidth, buttonHeight), "显示banner"))
        {
            if (isTimes)
            {
                isTimes = false;
                Yodo1U3dAds.SetBannerAlign(Yodo1U3dConstants.BannerAdAlign.BannerAdAlignBotton | Yodo1U3dConstants.BannerAdAlign.BannerAdAlignHorizontalCenter);
            }
            //展示Banner广告
            Yodo1U3dAds.ShowBanner();
        }

        if (GUI.Button(new Rect(Screen.width / 4, startHeight + buttonSpace + buttonHeight, buttonWidth, buttonHeight), "隐藏banner"))
        {
            //隐藏Banner广告
            Yodo1U3dAds.HideBanner();
        }
        if (GUI.Button(new Rect(Screen.width / 4, startHeight + buttonHeight * 2 + buttonSpace * 2, buttonWidth, buttonHeight), "显示插屏广告"))
        {
            //展示插屏广告
            if (Yodo1U3dAds.InterstitialIsReady())
            {
                Yodo1U3dAds.ShowInterstitial();
            }
            else
            {
                Debug.Log("插屏广告还没缓存好！");
            }
        }

        if (GUI.Button(new Rect(Screen.width / 4, startHeight + buttonHeight * 3 + buttonSpace * 3, buttonWidth, buttonHeight), "ShowAdVideo"))
        {
            //展示视频广告
            if (Yodo1U3dAds.VideoIsReady())
            {
                Yodo1U3dAds.ShowVideo();
            }
            else
            {
                Debug.Log("视频广告还没缓存好！");
            }
        }
    }
}