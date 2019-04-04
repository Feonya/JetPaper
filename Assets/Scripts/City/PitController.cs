using UnityEngine;

public class PitController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    public GameObject plane;
    private SpriteRenderer planeSpriteRenderer;
    private PlaneController planeController;
    public GameObject dropPosition;
    public AudioSource dropSound;

    public GameObject fakePlayer;
    private Animator fakePlayerAnimator;
    public GameObject fakePlane;
    private SpriteRenderer fakePlaneSpriteRenderer;

    private Transform pitTransform;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        planeController = plane.GetComponent<PlaneController>();
        planeSpriteRenderer = plane.GetComponent<SpriteRenderer>();

        fakePlayerAnimator = fakePlayer.GetComponent<Animator>();
        fakePlayerAnimator.runtimeAnimatorController = player.GetComponent<Animator>().runtimeAnimatorController; // 将当前player的动画控制器段赋给坑中的假人

        fakePlaneSpriteRenderer = fakePlane.GetComponent<SpriteRenderer>();
        fakePlayer.SetActive(false);
        fakePlane.SetActive(false);

        pitTransform = transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.pitWithPlayer = gameObject;

            playerController.Dead();
            playerController.Disappear();
            fakePlayer.SetActive(true);
            fakePlayerAnimator.SetBool("isDead", true);
            dropSound.Play();
        }
        if (collision.CompareTag("Plane"))
        {
            playerController.pitWithPlane = gameObject;

            playerController.Dead();
            planeController.Disappear();

            // 重新获取飞机的sprite，并将其传递给假飞机
            planeSpriteRenderer = plane.GetComponent<SpriteRenderer>();
            fakePlaneSpriteRenderer.sprite = planeSpriteRenderer.sprite;

            fakePlane.SetActive(true);
            dropSound.Play();
        }

        //pitTransform.SetAsFirstSibling(); // 有落坑物体的坑设置为pits下的第一个
    }

    public void DisableFakePlayer()
    {
        fakePlayer.SetActive(false);
    }

    public void DisableFakePlane()
    {
        fakePlane.SetActive(false);
    }
}