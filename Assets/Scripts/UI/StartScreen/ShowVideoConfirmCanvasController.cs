using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVideoConfirmCanvasController : MonoBehaviour
{
    public GameObject showVideoConfirmText;
    public GameObject showVideoConfirmText1;

    public GameObject showAdsFailed;

    private void OnEnable()
    {
        if (showAdsFailed.activeSelf)
        {
            showAdsFailed.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (GemController.Showed)
        {
            if (!showVideoConfirmText1.activeSelf)
            {
                showVideoConfirmText1.SetActive(true);
                showVideoConfirmText.SetActive(false);
            }
        }
        else
        {
            if (!showVideoConfirmText.activeSelf)
            {
                showVideoConfirmText.SetActive(true);
                showVideoConfirmText1.SetActive(false);
            }
        }
    }
}
