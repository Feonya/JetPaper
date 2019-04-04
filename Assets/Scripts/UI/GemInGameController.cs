using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GemInGameController : MonoBehaviour
{
    public Animation numberAnimation;
    public Text numberText;
    public StringBuilder numberTextString;

    private int preNumber;

    private void Start()
    {
        numberTextString = new StringBuilder("X ");
        numberText.text = numberTextString.Append(GemController.Number).ToString();
        preNumber = GemController.Number;
    }

    public void UpdateNumberText()
    {
        numberText.text = numberTextString.Replace(preNumber.ToString(), GemController.Number.ToString()).ToString();
        preNumber = GemController.Number;

        numberAnimation.Play();
    }
}
