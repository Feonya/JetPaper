using System.Collections;
using UnityEngine;

public class HandBombController : MonoBehaviour
{
    private Vector3 originalPosition;
    private Transform bombTransform;
    private Animator animator;

    public GameObject explosion;
    private Collider2D explosionCollider;
    public LayerMask whatIsPlayer;
    private bool onCollidePlayer;

    //public Rigidbody2D body;

    private GameObject player;
    private PlayerController playerController;

    public AudioSource explosionSound;

    private void Start()
    {
        originalPosition = transform.position;
        bombTransform = transform;
        animator = GetComponent<Animator>();

        explosionCollider = explosion.GetComponent<Collider2D>();
        onCollidePlayer = false;

        //body = GetComponent<Rigidbody2D>();

        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        CheckCollidePlayer();
    }

    private void ResetBomb()
    {
        //body.simulated = true;
        bombTransform.position = originalPosition;
        gameObject.SetActive(false);
        explosion.SetActive(false);
    }

    private void CheckCollidePlayer()
    {
        onCollidePlayer = explosionCollider.IsTouchingLayers(whatIsPlayer);

        if (onCollidePlayer)
        {
            playerController.Dead();
        }
    }

    public void CallWaitExplosion()
    {
        StartCoroutine(WaitExplosion());
    }

    private IEnumerator WaitExplosion()
    {
        yield return new WaitForSeconds(3.0f);
        //body.simulated = false;
        animator.Play("BombExplode");
        explosionSound.Play();
    }
}