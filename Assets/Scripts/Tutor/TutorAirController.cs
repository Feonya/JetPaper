using UnityEngine;

public class TutorAirController : MonoBehaviour
{
    public GameObject tutorPlane;
    private TutorPlaneController tutorPlaneController;
    private Rigidbody2D tutorPlaneBody;
    private CapsuleCollider2D capsuleCollider;

    public TutorPlayerController tutorPlayerController;

    private void Start()
    {
        gameObject.SetActive(false);
        tutorPlaneBody = tutorPlane.GetComponent<Rigidbody2D>();
        tutorPlaneController = tutorPlane.GetComponent<TutorPlaneController>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // 碰撞检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plane"))
        {
            tutorPlaneController.windforce = 1.0f;
            tutorPlaneController.onForcedIdleState = false;
            tutorPlaneController.EnableGravity();
            tutorPlaneBody.velocity += new Vector2(0.0f, 1.5f);

            tutorPlayerController.canMove = false;

            capsuleCollider.enabled = false;
        }
    }
}