using UnityEngine;

public class AirController : MonoBehaviour
{
    public GameObject plane;
    private PlaneController planeController;
    private CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        planeController = plane.GetComponent<PlaneController>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable()
    {
        capsuleCollider.enabled = true;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    // 碰撞检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plane"))
        {
            planeController.PushUp();

            capsuleCollider.enabled = false;
        }
    }
}