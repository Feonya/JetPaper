using UnityEngine;

public class FanController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerBody;

    public GameObject plane;
    private Rigidbody2D planeBody;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerBody = player.GetComponent<Rigidbody2D>();
        planeBody = plane.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerBody.AddForce(new Vector2(-100.0f, 0.0f));
        }

        if (collision.CompareTag("Plane"))
        {
            planeBody.AddForce(new Vector2(-100.0f, 0.0f));
        }
    }
}