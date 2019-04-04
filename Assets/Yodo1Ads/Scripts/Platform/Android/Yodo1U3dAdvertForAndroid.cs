using UnityEngine;

public class Yodo1U3dAdvertForAndroid
{
#if UNITY_ANDROID
    private static AndroidJavaClass jc = null;

    static Yodo1U3dAdvertForAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            jc = new AndroidJavaClass("com.yodo1.advert.unity.UnityYodo1Advertising");
        }
    }

    //显示插屏广告
    public static void showInterstitial(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("showInterstitial", gameObjectName, callbackName);
            }
        }
    }

    //是否已经缓存好插屏广告
    public static bool interstitialIsReady()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                bool value = jc.CallStatic<bool>("interstitialIsReady");
                return value;
            }
        }
        return false;
    }

    //播放视频广告
    public static void showVideo(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("showVideo", gameObjectName, callbackName);
            }
        }
    }

    //检查视频广告是否缓冲完成
    public static bool videoIsReady()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                bool value = jc.CallStatic<bool>("videoIsReady");
                return value;
            }
        }
        return false;
    }

    //显示Banner
    public static void ShowBanner(string gameObjectName, string callbackName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("ShowBanner", gameObjectName, callbackName);
            }
        }
    }

    //设置Banner
    public static void SetBannerAlign(Yodo1U3dConstants.BannerAdAlign align)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("SetBannerAlign", align);
            }
        }
    }

    //关闭Banner
    public static void RemoveBanner()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("RemoveBanner");
            }
        }
    }

    //隐藏Banner
    public static void HideBanner()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("HideBanner");
            }
        }
    }

#endif
}