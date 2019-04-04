using UnityEngine;

public class AboutCanvasController : MonoBehaviour
{
    public GameObject debugButton;
    private int debugClicked;
    public GameObject mainCanvas;

    private void OnDisable()
    {
        debugClicked = 0;
    }

    private void Start()
    {
        debugClicked = 0;
    }

    // 点击关于界面的返回按钮...
    public void OnAboutBackButtonClick()
    {
        gameObject.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void OnAboutContentClick()
    {
        if (debugClicked < 2)
        {
            debugClicked += 1;
        }
        else
        {
            debugButton.SetActive(true);
        }
    }
}