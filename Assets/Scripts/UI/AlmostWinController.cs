using UnityEngine;
using UnityEngine.SceneManagement;

public class AlmostWinController : MonoBehaviour
{
    public void BackToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }
}