using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YellowHatBoyComeOnStageController : MonoBehaviour
{
    public DidNotPlayButtonController didNotPlayButtonController;
    public PlaneController planeController;
    public GameObject foundButton;

    private GameObject player;
    private PlayerController playerController;

    private bool beFound;
    private bool startChat;

    private void Start()
    {
        beFound = AchievementsAndHighscoresController.achievementListRecorder.cardsName.Contains("YellowHatBoyCard");

        if (beFound)
        {
            Destroy(gameObject);
        }
        else
        {
            startChat = false;

            player = PlayerChooser.ChoosePlayer();
            playerController = player.GetComponent<PlayerController>();
        }
    }

    private void FixedUpdate()
    {
        if (startChat && playerController.onGround)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
            playerController.ForceIdle();
            playerController.DisableBlowButton();
            playerController.DisableJumpButton();
            didNotPlayButtonController.DisableDidNotPlayButton();
            
            if (!foundButton.activeSelf)
            {
                foundButton.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            planeController.ForceIdle();

            startChat = true;

            AchievementsAndHighscoresController.achievementListRecorder.EnableCard("YellowHatBoyCard");
        }
    }

    public void OnFoundButtonClick()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
