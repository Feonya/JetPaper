using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestDogController : MonoBehaviour
{
    public AudioSource dogSound;

    private GameObject player;
    private Transform playerTransform;

    private Transform forestDogTransform;

    private bool alive;
    private bool hunt;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerTransform = player.transform;

        forestDogTransform = transform;

        alive = false;
        hunt = false;
    }

    private void FixedUpdate()
    {
        if (!hunt)
        {
            if (alive)
            {
                Follow();
            }
            else
            {
                CheckPlayerInRange();
            }
        }
    }

    private void Follow()
    {
        if (forestDogTransform.position.x - playerTransform.position.x < 3.0f)
        {
            forestDogTransform.position += new Vector3(Time.fixedDeltaTime * 1.0f, 0.0f, 0.0f);

            if (forestDogTransform.position.x - playerTransform.position.x < - 2.0f)
            {
                hunt = true;
                Hunt();
            }
        }
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (forestDogTransform.position.x - playerTransform.position.x < 3.0f) // 如果玩家进入前方0个单位
        {
            alive = true;
        }
    }

    private void Hunt()
    {
        var body = GetComponent<Rigidbody2D>();

        body.simulated = true;
        GetComponent<Animator>().enabled = true;

        body.AddForce(new Vector2(Random.Range(150.0f, 350.0f), 300.0f));
        dogSound.Play();

        StartCoroutine(ChangeSortingLayer());
    }

    IEnumerator ChangeSortingLayer()
    {
        yield return new WaitForSeconds(0.5f);

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Foreground";
        spriteRenderer.sortingOrder = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().Dead();
        }
    }

    private void OnBecameInvisible()
    {
        if (hunt)
        {
            Destroy(gameObject);
        }
    }
}
