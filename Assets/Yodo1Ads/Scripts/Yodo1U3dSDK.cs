using System.Collections.Generic;
using UnityEngine;
using Yodo1U3dJSON;

public class Yodo1U3dSDK : MonoBehaviour
{
    private string _sdkGameObjectName = null;
    public const string sdkMethodName = "Yodo1U3dSDKCallBackResult";

    //ResultCode
    public const int RESULT_CODE_FAILED = 0;

    public const int RESULT_CODE_SUCCESS = 1;
    public const int RESULT_CODE_CANCEL = 2;

    private static Yodo1U3dSDK sharedInstance;

    public static Yodo1U3dSDK SharedInstance
    {
        get
        {
            if (!sharedInstance)
            {
                sharedInstance = (Yodo1U3dSDK)FindObjectOfType(typeof(Yodo1U3dSDK));
            }

            return sharedInstance;
        }
    }

    public string sdkGameObjectName
    {
        get
        {
            _sdkGameObjectName = Yodo1U3dSDK.SharedInstance.gameObject.name;
            return _sdkGameObjectName;
        }
    }

    #region advister delegate 广告

    //ShowInterstitialAd of delegate
    public delegate void InterstitialAdDelegate(Yodo1U3dConstants.AdEvent adEvent, string error);

    private static InterstitialAdDelegate _interstitialAdDelegate;

    public static void setInterstitialAdDelegate(InterstitialAdDelegate action)
    {
        _interstitialAdDelegate = action;
    }

    //ShowBanner of delegate
    public delegate void BannerdDelegate(Yodo1U3dConstants.AdEvent adEvent, string error);

    private static BannerdDelegate _bannerdDelegate;

    public static void setBannerdDelegate(BannerdDelegate action)
    {
        _bannerdDelegate = action;
    }

    //RewardVideo of delegate
    public delegate void RewardVideoDelegate(bool finished, string error);

    private static RewardVideoDelegate _rewardVideoDelegate;

    public static void setRewardVideoDelegate(RewardVideoDelegate action)
    {
        _rewardVideoDelegate = action;
    }

    #endregion advister delegate 广告

    public void Awake()
    {
        if (Global.PackageName != "com.tykj.jetpaper.tt" && Global.PackageName != "com.tykj.jetpaper.android")
        {
            //Destroy(GameObject.Find("Yodo1Ads"));
            Destroy(gameObject);
        }
        else
        {
            if (gameObject == SharedInstance.gameObject)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            Yodo1U3dAds.InitWithAppKey(Yodo1U3dAdsSettings.appKey);
        }
    }

    public void OnDestroy()
    {
        if (gameObject == SharedInstance.gameObject)
        {
            _interstitialAdDelegate = null;
            _bannerdDelegate = null;
            _rewardVideoDelegate = null;
        }
    }

    public void Yodo1U3dSDKCallBackResult(string result)
    {
        Debug.Log("Yodo1AdsCallBackResult-->result:" + result + "\n");
        Yodo1U3dConstants.Yodo1AdsType flag = Yodo1U3dConstants.Yodo1AdsType.Yodo1AdsTypeNone;
        int resultCode = 0;
        string error = "";
        Dictionary<string, object> obj = (Dictionary<string, object>)Yodo1JSON.Deserialize(result);
        if (obj != null)
        {
            if (obj.ContainsKey("resulType"))
            {
                flag = (Yodo1U3dConstants.Yodo1AdsType)int.Parse(obj["resulType"].ToString());  //判定来自哪个回调的标记
            }
            if (obj.ContainsKey("code"))
            {
                resultCode = int.Parse(obj["code"].ToString()); //结果码
            }
            if (obj.ContainsKey("error"))
            {
                error = obj["error"].ToString(); //msg
            }
        }

        switch (flag)
        {
            case Yodo1U3dConstants.Yodo1AdsType.Yodo1AdsTypeBanner:  //adview of banner
                {
                    if (_bannerdDelegate != null)
                    {
                        _bannerdDelegate(getAdEvent(resultCode), error);
                    }
                }
                break;

            case Yodo1U3dConstants.Yodo1AdsType.Yodo1AdsTypeInterstitial: //Interstitial
                {
                    if (_interstitialAdDelegate != null)
                    {
                        _interstitialAdDelegate(getAdEvent(resultCode), error);
                    }
                }
                break;

            case Yodo1U3dConstants.Yodo1AdsType.Yodo1AdsTypeVideo:
                {
                    if (_rewardVideoDelegate != null)
                    {
                        if (resultCode == 1)
                        {
                            _rewardVideoDelegate(true, error);
                        }
                        else if (resultCode == 0)
                        {
                            _rewardVideoDelegate(false, error);
                        }
                        else
                        {
                            Debug.Log("RewardVideo resultCode ： " + resultCode);
                        }
                    }
                }
                break;
        }
    }

    /// <summary>
    /// 获取广告事件
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Yodo1U3dConstants.AdEvent getAdEvent(int value)
    {
        switch (value)
        {
            case 0:
                {
                    return Yodo1U3dConstants.AdEvent.AdEventClose;
                }
            case 1:
                {
                    return Yodo1U3dConstants.AdEvent.AdEventFinish;
                }
            case 2:
                {
                    return Yodo1U3dConstants.AdEvent.AdEventClick;
                }
            case 3:
                {
                    return Yodo1U3dConstants.AdEvent.AdEventLoaded;
                }
            case 4:
                {
                    return Yodo1U3dConstants.AdEvent.AdEventShowSuccess;
                }
            case 5:
                {
                    return Yodo1U3dConstants.AdEvent.AdEventShowFail;
                }
            case 6:
                {
                    return Yodo1U3dConstants.AdEvent.AdEventPurchase;
                }
            case -1:
                {
                    return Yodo1U3dConstants.AdEvent.AdEventLoadFail;
                }
        }
        return Yodo1U3dConstants.AdEvent.AdEventLoadFail;
    }
}