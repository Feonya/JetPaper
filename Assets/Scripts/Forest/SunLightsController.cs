using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLightsController : MonoBehaviour
{
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;

    private GameObject player;
    private Transform playerTransform;

    private Transform sunLightsTransform;

    private bool startLighting;

    public int lightingLight;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.transform;

        sunLightsTransform = transform;

        startLighting = false;

        lightingLight = 3;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();

        if (startLighting)
        {
            if (light1 != null && !light1.activeSelf)
            {
                light1.SetActive(true);
                light2.SetActive(true);
                light3.SetActive(true);
            }

            if (lightingLight <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (sunLightsTransform.position.x <= playerTransform.position.x) // 如果玩家进入光照区
        {
            if (!startLighting)
            {
                startLighting = true;
            }
        }
    }
}
