using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButtonController : MonoBehaviour
{
    public void OnReplayButtonClick()
    {
        GemController.Showed = false;

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level1");
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level2");
        }
    }
}