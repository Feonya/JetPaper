using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class NurseButtonController : MonoBehaviour {

    public Text clickTimesText;
    public GameObject clickTimesBG;

    private Button nurseButton;

    private GameObject player;
    private PlayerController playerController;

    private StringBuilder clickNumberString;

    private int clickTimes;

    private bool hasNurse;

    private void Start()
    {
        hasNurse = AchievementsAndHighscoresController.achievementListRecorder.cardsName.Contains("WhiteNurseCard");

        if (hasNurse)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            nurseButton = GetComponent<Button>();

            player = PlayerChooser.ChoosePlayer();
            playerController = player.GetComponent<PlayerController>();

            clickNumberString = new StringBuilder("0", 2);

            clickTimes = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!playerController.onDeadState)
        {
            if (!nurseButton.interactable)
            {
                nurseButton.interactable = true;
            }
        }
        else
        {
            if (nurseButton.interactable)
            {
                nurseButton.interactable = false;
            }
        }
    }

    public void OnNurseButtonClick()
    {
        if (!clickTimesBG.activeSelf)
        {
            clickTimesBG.SetActive(true);
        }

        if (clickTimes < 10)
        {
            clickTimes += 1;
            clickNumberString.Remove(0, 1);
            clickTimesText.text = clickNumberString.Append(clickTimes).ToString();
        }
        else
        {
            AchievementsAndHighscoresController.achievementListRecorder.EnableCard("WhiteNurseCard");
        }
    }
}
