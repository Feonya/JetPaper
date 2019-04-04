using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    public GameObject plane;
    private PlaneController planeController;
    public Button didNotPlayButton;
    private DidNotPlayButtonController didNotPlayButtonController;

    public AudioSource countSound;
    public AudioSource goSound;

    private int currentNumber;
    private float countDown;

    private Text startTimerText;
    private Shadow startTimerTextShadow;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        planeController = plane.GetComponent<PlaneController>();
        didNotPlayButtonController = didNotPlayButton.GetComponent<DidNotPlayButtonController>();

        currentNumber = 3;
        countDown = 0.5f;

        startTimerText = gameObject.GetComponent<Text>();
        startTimerTextShadow = gameObject.GetComponent<Shadow>();
    }

    private void FixedUpdate()
    {
        countDown -= Time.fixedDeltaTime;

        if (countDown <= 0.0f)
        {
            countDown = 0.67f;

            switch (currentNumber)
            {
                case 3:
                    gameObject.GetComponent<Text>().text = "3";
                    countSound.Play();
                    currentNumber -= 1;
                    break;

                case 2:
                    gameObject.GetComponent<Text>().text = "2";
                    countSound.Play();
                    currentNumber -= 1;
                    break;

                case 1:
                    gameObject.GetComponent<Text>().text = "1";
                    countSound.Play();
                    currentNumber -= 1;
                    break;

                case 0:
                    playerController.EnableBlowButton();
                    playerController.EnableJumpButton();
                    playerController.OutOfForcedIdleState();

                    planeController.OutOfForcedIdleState();

                    didNotPlayButtonController.EnableDidNotPlayButton();

                    startTimerText.text = "*初花*";
                    startTimerTextShadow.effectColor = new Color(0.2666667f, 0.772549f, 0.3568628f); // 阴影变绿色

                    goSound.Play();

                    countDown = 1.0f;
                    currentNumber -= 1;
                    break;

                case -1:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}