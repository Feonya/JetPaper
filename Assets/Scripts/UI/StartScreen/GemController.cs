using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GemController : MonoBehaviour
{
    public static bool Showed;
    public static int Number;
    private int preNumber;

    public GameObject mainCanvas;
    public GameObject showVideoConfirmCanvas;

    public GameObject showAdsFailed;
    
    public Text numberText;
    private StringBuilder numberTextString;

    private void OnEnable()
    {
        UpdateNumberText();
    }

    private void Start()
    {
        // 如果没有GemNumber的存档字段
        if (!PlayerPrefs.HasKey("GemNumber"))
        {
            Number = 3;
            PlayerPrefs.SetInt("GemNumber", Number);
        }
        else
        {
            Number = PlayerPrefs.GetInt("GemNumber");
        }

        numberTextString = new StringBuilder("X ");
        numberText.text = numberTextString.Append(Number).ToString();
        preNumber = Number;
    }

    public void UpdateNumberText()
    {
        if (numberTextString != null && preNumber != Number)
        {
            numberText.text = numberTextString.Replace(preNumber.ToString(), Number.ToString()).ToString();
            preNumber = Number;
        }
    }

    public void OnAddButtonClick()
    {
        showVideoConfirmCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        if (Showed)
        {
            Showed = false;
        }
    }

    public void OnOKButtonClick()
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
            TalkingDataController.WatchedAds();
        }
    }

    public void OnBackButtonClick()
    {
        mainCanvas.SetActive(true);
        showVideoConfirmCanvas.SetActive(false);
        Showed = false;
    }

    public static void Add3Gem()
    {
        Number += 3;
        PlayerPrefs.SetInt("GemNumber", Number);
        Showed = true;
    }

    public static void Add10Gem()
    {
        Number += 10;
        PlayerPrefs.SetInt("GemNumber", Number);
    }
}
