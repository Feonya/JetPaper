using System.Collections;
using UnityEngine;

public class FinishHandler : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    public GameObject plane;
    private SpriteRenderer planeSpriteRenderer;
    private Transform planeTransform;
    private PlaneController planeController;
    private Vector3 direction;

    public GameObject littleGirl;
    private Transform littleGirlTransform;
    private Vector3 littleGirlPosition;
    private Animator littleGirlAnimator;

    public GameObject letter;
    private Animator letterAnimator;
    public GameObject evilLetter;
    private Animator evilLetterAnimator;

    public AudioSource finishMusic;

    private bool littleGirlAppeared;
    private bool planeFlyToLittleGirl;
    public bool littleGirlGetLetter;

    public GameObject finishScreen;

    public GameObject shoe;
    private Rigidbody2D shoeBody;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();

        planeSpriteRenderer = plane.GetComponent<SpriteRenderer>();
        planeTransform = plane.transform;
        planeController = plane.GetComponent<PlaneController>();

        littleGirlTransform = littleGirl.transform;
        littleGirlPosition = littleGirlTransform.position;
        littleGirlAnimator = littleGirl.GetComponent<Animator>();

        letterAnimator = letter.GetComponent<Animator>();
        evilLetterAnimator = evilLetter.GetComponent<Animator>();

        littleGirlAppeared = false;
        planeFlyToLittleGirl = false;
        littleGirlGetLetter = false;

        shoeBody = shoe.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerController.onFinishLine)
        {
            if (littleGirlGetLetter) // 小女孩得到了信...
            {
                if (planeController.hasAEvil)
                {
                    evilLetterAnimator.enabled = true; //激活恶魔信
                    littleGirlAnimator.Play("LittleGirlAnger"); // 播放小女孩发怒动画
                    StartCoroutine(ThrowShoe());
                }
                else
                {
                    letterAnimator.enabled = true; // 激活信
                    StartCoroutine(ShowFinishScreen());
                }

                plane.SetActive(false);
                planeFlyToLittleGirl = false;
                littleGirlGetLetter = false;
            }
            else if (!littleGirlAppeared) // 小女孩还未出现...
            {
                StartCoroutine(AppearLittleGirl());
            }
            else if (planeFlyToLittleGirl) // 飞机飞向小女孩
            {
                // 如果飞机在小女孩身后，则反转y轴
                if (planeTransform.position.x > littleGirlPosition.x)
                {
                    if (!planeSpriteRenderer.flipY)
                    {
                        planeSpriteRenderer.flipY = true;
                    }
                }

                planeTransform.position += direction * 0.03f;

                // 如果飞机的y坐标大于-2则开启物理模拟用于碰撞小女孩，而又保证不会碰到地面
                if (planeTransform.position.y > -2)
                {
                    planeController.EnablePhysicsSimulate();
                }
            }
        }
    }

    private IEnumerator AppearLittleGirl()
    {
        direction = (littleGirlPosition - planeTransform.position).normalized;
        littleGirlAppeared = true;
        littleGirl.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        planeFlyToLittleGirl = true;
        planeTransform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, new Vector3(0.0f, 0.0f, 1.0f)); // 朝向女孩
    }

    private IEnumerator ShowFinishScreen()
    {
        yield return new WaitForSeconds(3.0f);

        // 触发结束画面
        finishScreen.SetActive(true);
    }

    private IEnumerator ThrowShoe()
    {
        yield return new WaitForSeconds(3.0f);
        evilLetter.SetActive(false);
        shoe.SetActive(true);
        shoeBody.AddForce(new Vector2(-80.0f, 0.0f));
        shoeBody.AddTorque(20.0f);
    }
}