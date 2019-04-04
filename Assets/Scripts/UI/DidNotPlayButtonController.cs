using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DidNotPlayButtonController : MonoBehaviour
{
    public GameObject ExitOrQuitButtons;
    public GamePauser pauser;

    public GameObject rebirthConfirm;
    private bool onRebirthConfirmShowed;

    private void Start()
    {
        onRebirthConfirmShowed = false;

        DisableDidNotPlayButton();
        ExitOrQuitButtons.SetActive(false);
    }

    public void OnDidNotPlayButtonClick()
    {
        // 如果其他某些物体是激活的
        if (rebirthConfirm.activeSelf)
        {
            onRebirthConfirmShowed = true;
            rebirthConfirm.SetActive(false);
        }

        DisableDidNotPlayButton();

        ExitOrQuitButtons.SetActive(true);

        pauser.pauseGame();
    }

    public void OnExitButtonClick()
    {
        //TalkingDataController.CheckExitWithoutPlayed(); // 退出前检测是否进入过任意关卡
        Application.Quit();
    }

    public void OnQuitButtonClick()
    {
        SceneManager.LoadScene("StartScreen");

        pauser.resumeGame();
    }

    public void OnBackToGameButtonClick()
    {
        // 如果其他某些物体曾经是激活的
        if (onRebirthConfirmShowed)
        {
            onRebirthConfirmShowed = false;
            rebirthConfirm.SetActive(true);
        }

        EnableDidNotPlayButton();

        ExitOrQuitButtons.SetActive(false);

        pauser.resumeGame();
    }

    public void DisableDidNotPlayButton()
    {
        if (gameObject.GetComponent<Button>().interactable == true)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void EnableDidNotPlayButton()
    {
        if (gameObject.GetComponent<Button>().interactable == false)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }
}