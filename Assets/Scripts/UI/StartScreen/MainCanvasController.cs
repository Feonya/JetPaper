using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCanvasController : MonoBehaviour
{
    public Sprite soundOpenedImage;
    public Sprite soundClosedImage;
    public Button soundButton;
    public GameObject aboutCanvas;
    public GameObject levelSelectCanvas;
    public GameObject particles;
    public GameObject exitMessage;
    private bool exitMessageIsShowing;

    private void OnEnable()
    {
        if (particles.activeSelf)
        {
            particles.SetActive(false);
        }

        if (exitMessage.activeSelf)
        {
            exitMessage.SetActive(false);
            exitMessageIsShowing = false;

        }
    }

    // 初始化
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // 禁用休眠

        exitMessageIsShowing = false;

        CheckSound();
    }

    // 判断声音开关，据此加载图标
    private void CheckSound()
    {
        if (Global.SoundEnabled)
        {
            soundButton.GetComponent<Image>().sprite = soundOpenedImage;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundClosedImage;
        }
    }

    // 点击声音控制按钮...
    public void OnSoundButtonClick()
    {
        if (Global.SoundEnabled)
        {
            AudioListener.pause = true;
            Global.SoundEnabled = false;
            soundButton.GetComponent<Image>().sprite = soundClosedImage;
        }
        else
        {
            AudioListener.pause = false;
            Global.SoundEnabled = true;
            soundButton.GetComponent<Image>().sprite = soundOpenedImage;
        }
    }

    // 点击教学按钮...
    public void OnTutorButtonClick()
    {
        SceneManager.LoadScene("Tutor");
    }

    // 点击关于按钮...
    public void OnAboutButtonClick()
    {
        gameObject.SetActive(false);
        aboutCanvas.SetActive(true);
    }

    // 点击开始按钮...
    public void OnStartButtonClick()
    {
        gameObject.SetActive(false);
        levelSelectCanvas.SetActive(true);
    }

    // 点击退出按钮...
    public void OnExitButtonClick()
    {
        if (Global.PackageName == "com.tykj.jetpaper.nearme.gamecenter")
        {
            //AndroidJavaClass：通过指定类名可以构造出一个类
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            // UnityPlayer这个类可以获取当前的Activity
            // currentActivity字符串对应源码中UnityPlayer类下 的 Activity 变量名。
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

            // 在对象上调用一个Java方法
            jo.Call("OppoExit");
        }
        else
        {
            if (!exitMessageIsShowing)
            {
                exitMessage.SetActive(true);
                exitMessageIsShowing = true;

                StartCoroutine(DisappearExitMessage());
            }
            else if (exitMessageIsShowing)
            {
                //TalkingDataController.CheckExitWithoutPlayed(); // 退出前检测是否进入过任意关卡
                Application.Quit();
            }
        }
    }

    private IEnumerator DisappearExitMessage()
    {
        yield return new WaitForSeconds(2.0f);

        exitMessage.SetActive(false);
        exitMessageIsShowing = false;
    }
}