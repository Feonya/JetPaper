using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Corrector : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;

    public Transform foregroundCloudsTransform;
    public Transform backgroundCloudsTransform;

    private Scene currentScene;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.transform;

        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Level3")
        {
            playerTransform.GetChild(0).position -= new Vector3(0.0f, 1.0f, 0.0f);
        }
    }

    private void FixedUpdate()
    {
        foregroundCloudsTransform.position = new Vector2(foregroundCloudsTransform.position.x, playerTransform.position.y);
        backgroundCloudsTransform.position = new Vector2(backgroundCloudsTransform.position.x, playerTransform.position.y);
    }
}
