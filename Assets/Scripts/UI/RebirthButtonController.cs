using UnityEngine;

public class RebirthButtonController : MonoBehaviour
{
    public GemInGameController gemInGameController;
    public GameObject rebirthConfirm;

    private PlayerController playerController;

    private void Start()
    {
        playerController = PlayerChooser.ChoosePlayer().GetComponent<PlayerController>();
    }

    public void onRebirthButtonClick()
    {
        playerController.Rebirth();

        GemController.Number -= 1;
        GemController.Showed = false;
        PlayerPrefs.SetInt("GemNumber", GemController.Number);

        gemInGameController.UpdateNumberText();

        rebirthConfirm.SetActive(false);
    }
}