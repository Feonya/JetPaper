using UnityEngine;

public class SwarmController : MonoBehaviour
{
    public GameObject plane;
    private Rigidbody2D planeBody;

    private Transform swarmTransform;
    private Vector2 originalPosition;

    private void Start()
    {
        swarmTransform = transform;
        originalPosition = swarmTransform.localPosition;

        planeBody = plane.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        swarmTransform.localPosition = originalPosition + new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plane"))
        {
            planeBody.velocity -= new Vector2(0.0f, 0.6f);
        }
    }
}