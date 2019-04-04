/// 本脚本PlayerPrefs用了两个键："ChoosenPlayer"、"CharactorsActive"

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorSelectCanvasController : MonoBehaviour
{
    public GameObject mainCanvas;

    private GameObject charactorsList;
    private Transform charactorsListTransform;

    private GameObject charactorsPool;
    private Transform charactorsPoolTransform;

    private string choosenPlayer;

    private int charactorsNumber;

    [Serializable]
    public class CharactorsActiveRecorder
    {
        public List<string> charactorsName;

        public void EnableCharactor(string name)
        {
            if (!charactorsName.Contains(name))
            {
                charactorsName.Add(name);

                string charactorsListJson = JsonUtility.ToJson(charactorsActiveRecorder);
                PlayerPrefs.SetString("CharactorsActive", charactorsListJson);
            }
        }
    }

    public static CharactorsActiveRecorder charactorsActiveRecorder;

    private void Awake()
    {
        choosenPlayer = PlayerPrefs.GetString("ChoosenPlayer");

        charactorsList = transform.Find("CharactorsScroller/CharactorsList").gameObject;
        charactorsListTransform = charactorsList.transform;

        charactorsPool = transform.Find("CharactorsPool").gameObject;
        charactorsPoolTransform = charactorsPool.transform;
    }

    private void OnEnable()
    {
        InitCharactorsList();
    }

    private void Start()
    {
        charactorsListTransform.Find(choosenPlayer).GetComponent<Toggle>().isOn = true;
        gameObject.SetActive(false);
    }

    // --------------------------------------每添加一个角色，要添加一个方法
    public void OnGreenHatBoyHighlight()
    {
        choosenPlayer = "GreenHatBoy";
    }

    public void OnApeManHighlight()
    {
        choosenPlayer = "ApeMan";
    }

    public void OnYellowHatBoyHighlight()
    {
        choosenPlayer = "YellowHatBoy";
    }

    public void OnWhiteNurseHighlight()
    {
        choosenPlayer = "WhiteNurse";
    }

    public void OnColoredEggHighlight()
    {
        choosenPlayer = "ColoredEgg";
    }

    public void OnCharactorSelectBackButtonClick()
    {
        PlayerPrefs.SetString("ChoosenPlayer", choosenPlayer);

        mainCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void InitCharactorsList()
    {
        charactorsActiveRecorder = JsonUtility.FromJson<CharactorsActiveRecorder>(PlayerPrefs.GetString("CharactorsActive"));

        if (charactorsActiveRecorder == null)
        {
            charactorsActiveRecorder = new CharactorsActiveRecorder
            {
                charactorsName = new List<string>
                {
                    "GreenHatBoy"
                }
            };

            SaveCharactorsList();
        }

        charactorsNumber = charactorsActiveRecorder.charactorsName.Count;

        for (int i = 0; i < charactorsNumber; i++)
        {
            Transform charactor = charactorsPoolTransform.Find(charactorsActiveRecorder.charactorsName[i]);

            if (charactor != null)
            {
                charactor.SetParent(charactorsListTransform);
            }
        }

        //if (charactorsActiveRecorder == null)
        //{
        //    charactorsActiveRecorder = new CharactorsActiveRecorder();
        //    charactorsActiveRecorder.charactorsActive = new bool[charactorsNumber];
        //    for (int i = 0; i < charactorsNumber; i++)
        //    {
        //        charactorsActiveRecorder.charactorsActive[i] = charactorsListTransform.GetChild(i).gameObject.activeSelf;
        //    }
        //    SaveCharactorsList();
        //}
        //else
        //{
        //    int charactorsActiveLength = charactorsActiveRecorder.charactorsActive.Length;

        //    if (charactorsActiveLength == charactorsNumber)
        //    {
        //        for (int i = 0; i < charactorsNumber; i++)
        //        {
        //            charactorsListTransform.GetChild(i).gameObject.SetActive(charactorsActiveRecorder.charactorsActive[i]);
        //        }
        //    }
        //    else if (charactorsActiveLength < charactorsNumber) // 如果新版本有新的角色添加进来（也就是编辑器中比持久化数据存档数组长）
        //    {
        //        bool[] newCharactorsActive = new bool[charactorsNumber];
        //        for (int i = 0; i < charactorsNumber; i++)
        //        {
        //            newCharactorsActive[i] = charactorsListTransform.GetChild(i).gameObject.activeSelf;
        //        }
        //        charactorsActiveRecorder.charactorsActive = newCharactorsActive;
        //        SaveCharactorsList();
        //    }
        //}
    }

    private void SaveCharactorsList()
    {
        string charactorsListJson = JsonUtility.ToJson(charactorsActiveRecorder);
        PlayerPrefs.SetString("CharactorsActive", charactorsListJson);
    }
}