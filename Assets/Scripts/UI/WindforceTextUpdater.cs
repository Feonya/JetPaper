using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class WindforceTextUpdater : MonoBehaviour
{
    private int value;
    private int oldValue;
    private Text windforceText;
    private StringBuilder windforceTextString;

    private void Awake()
    {
        oldValue = 0;
        windforceText = GetComponent<Text>();
        windforceTextString = new StringBuilder("风力：0");
    }

    public void CheckWindforce(float windforce)
    {
        value = Mathf.RoundToInt(windforce * 20.0f + 20.0f); // 风力值由0.5~3.0转换为30~80

        windforceText.text = windforceTextString.Replace(oldValue.ToString(), value.ToString()).ToString();

        oldValue = value;
    }
}