using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Corrector : MonoBehaviour
{
    private GameObject player;

    private Scene currentScene;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();

        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Level2")
        {
            player.transform.GetChild(0).position -= new Vector3(0.0f, 0.966f, 0.0f);
            player.transform.GetChild(2).GetComponent<Camera>().backgroundColor = new Color(0.7803922f, 0.6392157f, 0.8823529f);
        }
    }
}
