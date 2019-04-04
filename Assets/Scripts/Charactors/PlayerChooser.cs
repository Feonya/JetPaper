using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChooser : MonoBehaviour
{
    public static GameObject player;

    private string playerName;

    private void Awake()
    {
        playerName = PlayerPrefs.GetString("ChoosenPlayer");

        // 选择角色-----------------------------------这里没添加一个新角色就要添加一个选择
        switch (playerName)
        {
            case "GreenHatBoy":
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                transform.GetChild(4).gameObject.SetActive(false);
                break;

            case "ApeMan":
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                transform.GetChild(4).gameObject.SetActive(false);
                break;

            case "YellowHatBoy":
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(false);
                transform.GetChild(4).gameObject.SetActive(false);
                break;

            case "WhiteNurse":
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(4).gameObject.SetActive(false);
                break;

            case "ColoredEgg":
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                transform.GetChild(4).gameObject.SetActive(true);
                break;
        }
    }

    public static GameObject ChoosePlayer()
    {
        player = GameObject.FindWithTag("Player");

        return player;
    }
}
