using UnityEngine;

//工具接口
public class Yodo1U3dInitForAndroid
{
#if UNITY_ANDROID
    private static AndroidJavaClass jc = null;

    static Yodo1U3dInitForAndroid()
    {
        jc = new AndroidJavaClass("com.yodo1.advert.unity.UnityYodo1SDK");
    }

    /// <summary>
    /// 初始化
    /// </summary>
	/// <param name="gameAppKey"></param>
    /// <returns></returns>
	public static void InitWithAppKey(string appKey)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject activityContext = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
                if (jc != null)
                {
                    jc.CallStatic("initSDK", activityContext, appKey);
                }
            }
        }
    }

    /// <summary>
    /// 设置log是否有效
    /// </summary>
    /// <param name="enable"></param>
    /// <returns></returns>
    public static bool SetLogEnable(bool enable)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("setLogEnable", enable);
            }
        }

        return false;
    }

    /// onCreate   OnApplicationPause（false）
    public static void onResume()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("onResume");
            }
        }
    }

    /// onCreate   OnApplicationPause（true）
    public static void onPause()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("onPause");
            }
        }
    }

    /// onCreate   onDestroy
    public static void onDestroy()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (jc != null)
            {
                jc.CallStatic("onDestroy");
            }
        }
    }

#endif
}