using UnityEngine;

public class Yodo1U3dAds : MonoBehaviour
{
    /// <summary>
    /// 初始化SDK
    /// </summary>
    public static void InitWithAppKey(string appKey)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
			Yodo1U3dAdvertForIOS.InitWithAppKey (appKey);
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            Yodo1U3dInitForAndroid.InitWithAppKey(appKey);
#endif
        }
    }

    /// <summary>
    /// 设置是否开启Log
    /// </summary>
    /// <returns><c>true</c>, if has ad video was unityed, <c>false</c> otherwise.</returns>
    public static bool SetLogEnable(bool enable)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
			return	Yodo1U3dAdvertForIOS.SetLogEnable(enable);
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            Yodo1U3dInitForAndroid.SetLogEnable(enable);
#endif
        }
        return false;
    }

    #region Banner

    /// <summary>
    /// 设置广告显示位置
    /// </summary>
    /// <param name="align">Align.</param>
    public static void SetBannerAlign(Yodo1U3dConstants.BannerAdAlign align)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
			Yodo1U3dAdvertForIOS.SetBannerAlign(align);
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            Yodo1U3dAdvertForAndroid.SetBannerAlign(align);
#endif
        }
    }

    /// <summary>
    /// 设置广告位置偏移量
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public static void SetBannerOffset(float x, float y)
    {
#if UNITY_ANDROID

#elif UNITY_IPHONE
		Yodo1U3dAdvertForIOS.SetBannerOffset(x,y);
#endif
    }

    /// <summary>
    /// 设置Banner广告缩放倍数.
    /// </summary>
    /// <param name="sx">Sx.</param>
    /// <param name="sy">Sy.</param>
    public static void SetBannerScale(float sx, float sy)
    {
#if UNITY_ANDROID

#elif UNITY_IPHONE
		Yodo1U3dAdvertForIOS.SetBannerScale(sx,sy);
#endif
    }

    /// <summary>
    /// 显示广告
    /// </summary>
    public static void ShowBanner()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
			Yodo1U3dAdvertForIOS.ShowBanner();
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            Yodo1U3dAdvertForAndroid.ShowBanner(Yodo1U3dSDK.SharedInstance.sdkGameObjectName, Yodo1U3dSDK.sdkMethodName);
#endif
        }
    }

    /// <summary>
    /// 隐藏广告
    /// </summary>
    public static void HideBanner()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
			Yodo1U3dAdvertForIOS.HideBanner();
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            Yodo1U3dAdvertForAndroid.HideBanner();
#endif
        }
    }

    /// <summary>
    /// 移除广告
    /// </summary>
    public static void RemoveBanner()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
			Yodo1U3dAdvertForIOS.RemoveBanner();
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            Yodo1U3dAdvertForAndroid.RemoveBanner();
#endif
        }
    }

    #endregion Banner

    #region Interstitial

    /// <summary>
    /// 插屏广告是否可以播放
    /// </summary>
    /// <returns><c>true</c>, if switch full screen ad was unityed, <c>false</c> otherwise.</returns>
    public static bool InterstitialIsReady()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
			return Yodo1U3dAdvertForIOS.InterstitialIsReady();
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            return Yodo1U3dAdvertForAndroid.interstitialIsReady();
#endif
        }
        return false;
    }

    /// <summary>
    /// 显示插屏广告
    /// </summary>
    public static void ShowInterstitial()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
			Yodo1U3dAdvertForIOS.ShowInterstitial();
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            Yodo1U3dAdvertForAndroid.showInterstitial(Yodo1U3dSDK.SharedInstance.sdkGameObjectName, Yodo1U3dSDK.sdkMethodName);
#endif
        }
    }

    #endregion Interstitial

    #region Video

    /// <summary>
    /// Video是否已经准备好
    /// </summary>
    /// <returns><c>true</c>, if switch ad video was unityed, <c>false</c> otherwise.</returns>
    public static bool VideoIsReady()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
			return	Yodo1U3dAdvertForIOS.VideoIsReady();
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            return Yodo1U3dAdvertForAndroid.videoIsReady();
#endif
        }
        return false;
    }

    /// <summary>
    /// 显示Video广告
    /// </summary>
    /// <param name="callbackGameObj">Callback game object.</param>
    /// <param name="callbackMethod">Callback method.</param>
    public static void ShowVideo()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IPHONE
			Yodo1U3dAdvertForIOS.ShowVideo ();
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            Yodo1U3dAdvertForAndroid.showVideo(Yodo1U3dSDK.SharedInstance.sdkGameObjectName, Yodo1U3dSDK.sdkMethodName);
#endif
        }
    }

    #endregion Video
}