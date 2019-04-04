using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreenController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    public Text finishScoreText;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        finishScoreText.text = "您的最终得分是：<color=#e75952>" + playerController.scoreNumber.ToString() + "</color> 分";

        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void BackToStartScreen()
    {
        if (canvasGroup.alpha == 1.0f)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }
}