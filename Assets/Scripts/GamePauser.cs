using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePauser : MonoBehaviour
{
    public Button blowButton;
    private EventTrigger blowButtonEventTrigger;
    public Button jumpButton;
    private EventTrigger jumpButtonEventTrigger;

    private PlayerController playerController;

    private void Start()
    {
        blowButtonEventTrigger = blowButton.GetComponent<EventTrigger>();
        jumpButtonEventTrigger = jumpButton.GetComponent<EventTrigger>();

        playerController = PlayerChooser.ChoosePlayer().GetComponent<PlayerController>();
    }

    public void pauseGame()
    {
        Time.timeScale = 0.0f;

        blowButton.interactable = false;
        blowButtonEventTrigger.enabled = false;

        jumpButton.interactable = false;
        jumpButtonEventTrigger.enabled = false;
    }

    public void resumeGame()
    {
        Time.timeScale = 1.0f;

        if (!playerController.onDeadState)
        {
            blowButton.interactable = true;
            blowButtonEventTrigger.enabled = true;

            jumpButton.interactable = true;
            jumpButtonEventTrigger.enabled = true;
        }
    }
}