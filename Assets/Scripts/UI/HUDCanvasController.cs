using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvasController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    public GameObject plane;
    private PlaneController planeController;
    private GameObject air;

    #region 力量条

    public GameObject powerBar;
    private float powerBarAmount; // 力量条的显示，值0~1
    private float powerBarAmountIncrement;
    private float powerValue; // 力量条对于的吹力，值为最小50
    private float powerValueIncrement;
    private Image powerValueImage;

    #endregion 力量条

    #region 测试FPS

    //public GameObject replayButton;
    //public GameObject showAdsFailed;

    //public Text fpsText;
    //private StringBuilder fpsTextString = new StringBuilder("FPS:x");
    //private string fpsNumericalString;
    //private string oldFpsNumericalString = "x";

    //private float refreshTime = 1.0f;

    #endregion 测试FPS

    private void Start()
    {
        TalkingDataController.GamePlayed = true;

        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        planeController = plane.GetComponent<PlaneController>();
        air = player.transform.Find("Air").gameObject;

        //powerValue = 50.0f; // 吹力的值再加50
        powerValue = playerController.powerValue;
        //powerValueIncrement = (120.0f / 100.0f) * 2.0f;
        powerValueIncrement = playerController.powerValueIncrement;
        powerBarAmount = 0.0f;
        //powerBarAmountIncrement = (120.0f / 100.0f) * 0.01f;
        powerBarAmountIncrement = playerController.powerBarAmountIncrement;
        powerValueImage = powerBar.GetComponent<Image>();
    }

    //private void Update()
    //{
    //    GetFPS();
    //}

    // 在吹气结束后调用此方法
    public void ResetPower()
    {
        powerBarAmount = 0.0f;
        powerValueImage.fillAmount = powerBarAmount;
        //powerValue = 50.0f;
        powerValue = playerController.powerValue;
        air.transform.localScale = Vector3.one;
    }

    // 在吸气状态调用此方法
    public void IncreasePower()
    {
        if (powerBarAmount < 1.0f)
        {
            powerBarAmount += powerBarAmountIncrement;
            powerValueImage.fillAmount = powerBarAmount;
            powerValue += powerValueIncrement;
        }
    }

    // 点击跳跃按钮...
    public void OnJumpButtonPress()
    {
        playerController.Jump();
    }

    public void OnJumpButtonRelease()
    {
        playerController.DisableJumpButton();
    }

    // 按下吹气按钮...
    public void OnBlowButtonPress()
    {
        playerController.Inhale();
    }

    // 释放吹气按钮...
    public void OnBlowButtonRelease()
    {
        playerController.Blow();
        planeController.GetPower(powerValue);
        air.transform.localScale = new Vector3(air.transform.localScale.x * powerBarAmount * 1.3f + 1.0f,
                                               air.transform.localScale.y * powerBarAmount * 1.3f + 1.0f,
                                               1.0f);
    }

    // *************************************测试用
    //private void GetFPS()
    //{
    //    refreshTime -= Time.deltaTime;
    //    if (refreshTime <= 0.0)
    //    {
    //        fpsNumericalString = (Time.timeScale / Time.deltaTime).ToString();
    //        fpsText.text = fpsTextString.Replace(oldFpsNumericalString, fpsNumericalString).ToString();
    //        oldFpsNumericalString = fpsNumericalString;

    //        refreshTime = 1.0f;
    //    }
    //}

    //public void OnDebugRebirthButtonClick()
    //{
    //    replayButton.SetActive(false);
    //    showAdsFailed.SetActive(false);

    //    playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    //    playerController.Rebirth();
    //}

    // *************************************测试用
}