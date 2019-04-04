using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReirthConfirmController : MonoBehaviour
{
    public GemInGameController gemInGameController;

    public GameObject rebirthButton;
    public GameObject showAdsButton;

    public GameObject canRebirthText;
    public GameObject canRebirthText1;

    public GameObject showAdsFailed;

    private void FixedUpdate()
    {
        if (GemController.Showed)
        {
            if (showAdsButton.activeSelf)
            {
                rebirthButton.SetActive(true);
                showAdsButton.SetActive(false);
            }

            if (!canRebirthText1.activeSelf)
            {
                canRebirthText1.SetActive(true);
                canRebirthText.SetActive(false);

                gemInGameController.UpdateNumberText();
            }
        }
        else
        {
            if (!canRebirthText.activeSelf)
            {
                canRebirthText1.SetActive(false);
                canRebirthText.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        if (showAdsFailed.activeSelf)
        {
            showAdsFailed.SetActive(false);
        }

        if (GemController.Number > 0)
        {
            if (!rebirthButton.activeSelf)
            {
                rebirthButton.SetActive(true);
                showAdsButton.SetActive(false);
            }
        }
        else
        {
            if (!showAdsButton.activeSelf)
            {
                rebirthButton.SetActive(false);
                showAdsButton.SetActive(true);
            }
        }
    }
}
