using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectCanvasController : MonoBehaviour
{
    public GameObject mainCanvas;

    // 点击关卡选择界面的返回按钮...
    public void OnLevelBackButtonClick()
    {
        gameObject.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void OnLevel1ButtonClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnLevel2ButtonClick()
    {
        SceneManager.LoadScene("Level2");
    }
}