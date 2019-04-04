using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject blinder;
    public AudioSource blindSound;

    private GameObject player;
    private Transform playerTransform;
    private PlayerController playerController;

    private Transform lightTransform;

    private SunLightsController sunLightsController;

    private int times;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.transform;
        playerController = player.GetComponent<PlayerController>();

        lightTransform = transform;

        sunLightsController = lightTransform.parent.GetComponent<SunLightsController>();

        times = 4;

        var randPosX = Random.Range(-10.0f, 5.0f);
        lightTransform.position = new Vector2(playerTransform.position.x + randPosX, lightTransform.position.y);
    }

    private void FixedUpdate()
    {
        if (times < 0)
        {
            sunLightsController.lightingLight -= 1;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!blinder.activeSelf && !playerController.onDeadState && player.name != "PlayerYellowHatBoy")
            {
                blinder.SetActive(true);
                blindSound.Play();
            }
        }
    }

    public void Appear()
    {
        times -= 1;

        var randPosX = Random.Range(-10.0f, 5.0f);
        lightTransform.position = new Vector2(playerTransform.position.x + randPosX, lightTransform.position.y);
    }
}
