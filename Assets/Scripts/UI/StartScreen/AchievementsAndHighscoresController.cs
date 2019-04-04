/// 本脚本PlayerPrefs用了两个键："AchievementListRecorder"、"CharactorsActive"

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsAndHighscoresController : MonoBehaviour
{
    public Transform frameTargetTransform;
    private Transform frameTransform;
    private Vector2 frameOriginalPosition;

    public Toggle achievementsToggle;
    public GameObject achievementsScroller;
    public Toggle highscoresToggle;
    public GameObject highscoresBoard;

    #region 和成就卡片列表有关的定义

    [Serializable]
    public class AchievementListRecorder
    {
        public List<string> cardsName;
        public List<bool> cardsClickable;

        public int newCardNumber;

        public void EnableCard(string name)
        {
            if (!cardsName.Contains(name))
            {
                DisableTwoTestCards();

                cardsName.Add(name);
                cardsClickable.Add(true);
                newCardNumber += 1;

                string achievementListJson = JsonUtility.ToJson(achievementListRecorder);
                PlayerPrefs.SetString("AchievementListRecorder", achievementListJson);

                Global.AchievementSound.Play();
            }
        }

        public void DisableTwoTestCards()
        {
            if (cardsName.Contains("NoAchievementCard")) // 两个测试卡片一起消失，所以只检测第一张卡片是否已消失
            {
                if (cardsClickable[cardsName.IndexOf("NoAchievementCard")])
                {
                    newCardNumber -= 1;
                }
                if (cardsClickable[cardsName.IndexOf("FakeNewManCard")])
                {
                    newCardNumber -= 1;
                }

                cardsClickable.Clear();
                cardsName.Clear();
            }
        }
    }

    private int activeCardNumber;

    public static AchievementListRecorder achievementListRecorder;
    public GameObject achievementsList;
    private Transform achievementsListTransform;
    public GameObject achievementsPool;
    private Transform achievementsPoolTransform;

    public GameObject newAchievementMark;

    public GameObject particles;

    #endregion 和成就卡片列表有关的定义

    private float moveSpeed;

    private bool onMoveIn;
    private bool onMoveOut;

    private void Start()
    {
        achievementsListTransform = achievementsList.transform;

        achievementsPoolTransform = achievementsPool.transform;

        frameTransform = transform;
        frameOriginalPosition = transform.position;

        moveSpeed = 0.5f;

        onMoveIn = false;
        onMoveOut = false;

        InitAchievementList();

        achievementsScroller.SetActive(false);
        highscoresBoard.SetActive(false);
    }

    private void FixedUpdate()
    {
        MoveIn();
        MoveOut();
        CheckNewMark();
    }

    private void MoveIn()
    {
        if (onMoveIn)
        {
            frameTransform.position = Vector2.MoveTowards(frameTransform.position, frameTargetTransform.position, moveSpeed);
        }
    }

    private void MoveOut()
    {
        if (onMoveOut)
        {
            frameTransform.position = Vector2.MoveTowards(frameTransform.position, frameOriginalPosition, moveSpeed);
        }
    }

    public void OnAchievementToggleClick()
    {
        if (achievementsToggle.isOn)
        {
            onMoveIn = true;
            onMoveOut = false;
        }
        else if (!highscoresToggle.isOn)
        {
            onMoveIn = false;
            onMoveOut = true;
        }
    }

    public void OnHighscoresToggleClick()
    {
        if (highscoresToggle.isOn)
        {
            onMoveIn = true;
            onMoveOut = false;
        }
        else if (!achievementsToggle.isOn)
        {
            onMoveIn = false;
            onMoveOut = true;
        }
    }

    private void CheckNewMark()
    {
        if (achievementListRecorder.newCardNumber > 0)
        {
            if (!newAchievementMark.activeSelf)
            {
                newAchievementMark.SetActive(true);
                SaveAchievementList();
            }
        }
        else if (achievementListRecorder.newCardNumber == 0)
        {
            if (newAchievementMark.activeSelf)
            {
                newAchievementMark.SetActive(false);
                SaveAchievementList();
            }
        }
    }

    // 初始化成就列表存档
    public void InitAchievementList()
    {
        achievementListRecorder = JsonUtility.FromJson<AchievementListRecorder>(PlayerPrefs.GetString("AchievementListRecorder"));

        if (achievementListRecorder == null) // 如果是首次进入游戏
        {
            // achievementListRecorder初始化
            achievementListRecorder = new AchievementListRecorder
            {
                cardsName = new List<string>
                {
                    "NoAchievementCard",
                    "FakeNewManCard"
                },
                cardsClickable = new List<bool>
                {
                    true,
                    true
                },
                newCardNumber = 2
            };

            SaveAchievementList();
        }

        activeCardNumber = achievementListRecorder.cardsName.Count;

        for (int i = 0; i < activeCardNumber; i++)
        {
            Transform card = achievementsPoolTransform.Find(achievementListRecorder.cardsName[i]);

            if (card != null)
            {
                card.GetComponent<Button>().interactable = 
                    achievementListRecorder.cardsClickable[achievementListRecorder.cardsName.IndexOf(achievementListRecorder.cardsName[i])];
                card.SetParent(achievementsListTransform);
            }
        }



        //if (achievementListRecorder == null)
        //{
        //    achievementListRecorder = new AchievementListRecorder();
        //    achievementListRecorder.cardsActive = new bool[cardsLength];
        //    achievementListRecorder.cardsCanClick = new bool[cardsLength];
        //    achievementListRecorder.newCardNumber = 2;

        //    for (int i = 0; i < cardsLength; i++)
        //    {
        //        achievementListRecorder.cardsActive[i] = achievementsListTransform.GetChild(i).gameObject.activeSelf;
        //        achievementListRecorder.cardsCanClick[i] = achievementsListTransform.GetChild(i).GetComponent<Button>().interactable;
        //    }

        //    SaveAchievementList();
        //}
        //else
        //{
        //    int cardsActiveLength = achievementListRecorder.cardsActive.Length;

        //    for (int i = 0; i < cardsActiveLength; i++)
        //    {
        //        achievementsListTransform.GetChild(i).gameObject.SetActive(achievementListRecorder.cardsActive[i]);
        //        achievementsListTransform.GetChild(i).GetComponent<Button>().interactable = achievementListRecorder.cardsCanClick[i];
        //    }

        //    if (cardsActiveLength < cardsLength)
        //    {
        //        bool[] newCardsActive = new bool[cardsLength];
        //        bool[] newCardsCanClick = new bool[cardsLength];
        //        for (int i = 0; i < cardsLength; i++)
        //        {
        //            newCardsActive[i] = achievementsListTransform.GetChild(i).gameObject.activeSelf;
        //            newCardsCanClick[i] = achievementsListTransform.GetChild(i).GetComponent<Button>().interactable;
        //        }
        //        achievementListRecorder.cardsActive = newCardsActive;
        //        achievementListRecorder.cardsCanClick = newCardsCanClick;
        //        SaveAchievementList();
        //    }
        //}
    }

    // 保存成就列表存档
    public void SaveAchievementList()
    {
        string achievementListJson = JsonUtility.ToJson(achievementListRecorder);
        PlayerPrefs.SetString("AchievementListRecorder", achievementListJson);
    }

    //-------------------------------------这里注意每添加一个卡片就要增加一个方法

    #region 点击成就卡片领取奖励的动作

    public void OnNoAchievementCardClick()
    {
        achievementsListTransform.GetChild(achievementListRecorder.cardsName.IndexOf("NoAchievementCard")).GetComponent<Button>().interactable = false;
        achievementListRecorder.cardsClickable[achievementListRecorder.cardsName.IndexOf("NoAchievementCard")] = false;

        CardClick();
    }

    public void OnFakeNewManCardClick()
    {
        achievementsListTransform.GetChild(achievementListRecorder.cardsName.IndexOf("FakeNewManCard")).GetComponent<Button>().interactable = false;
        achievementListRecorder.cardsClickable[achievementListRecorder.cardsName.IndexOf("FakeNewManCard")] = false;

        CardClick();
    }

    public void OnApeManCardClick()
    {
        CharactorSelectCanvasController.charactorsActiveRecorder.EnableCharactor("ApeMan");
        
        achievementsListTransform.GetChild(achievementListRecorder.cardsName.IndexOf("ApeManCard")).GetComponent<Button>().interactable = false;
        achievementListRecorder.cardsClickable[achievementListRecorder.cardsName.IndexOf("ApeManCard")] = false;

        CardClick();
    }

    public void OnYellowHatBoyCardClick()
    {
        CharactorSelectCanvasController.charactorsActiveRecorder.EnableCharactor("YellowHatBoy");
        
        achievementsListTransform.GetChild(achievementListRecorder.cardsName.IndexOf("YellowHatBoyCard")).GetComponent<Button>().interactable = false;
        achievementListRecorder.cardsClickable[achievementListRecorder.cardsName.IndexOf("YellowHatBoyCard")] = false;

        CardClick();
    }

    public void OnWhiteNurseCardClick()
    {
        CharactorSelectCanvasController.charactorsActiveRecorder.EnableCharactor("WhiteNurse");

        achievementsListTransform.GetChild(achievementListRecorder.cardsName.IndexOf("WhiteNurseCard")).GetComponent<Button>().interactable = false;
        achievementListRecorder.cardsClickable[achievementListRecorder.cardsName.IndexOf("WhiteNurseCard")] = false;

        CardClick();
    }

    public void OnColoredEggCardClick()
    {
        CharactorSelectCanvasController.charactorsActiveRecorder.EnableCharactor("ColoredEgg");

        achievementsListTransform.GetChild(achievementListRecorder.cardsName.IndexOf("ColoredEggCard")).GetComponent<Button>().interactable = false;
        achievementListRecorder.cardsClickable[achievementListRecorder.cardsName.IndexOf("ColoredEggCard")] = false;

        CardClick();
    }

    public void OnTenGemsPerDayCardClick()
    {
        achievementsListTransform.GetChild(achievementListRecorder.cardsName.IndexOf("TenGemsPerDayCard")).GetComponent<Button>().interactable = false;
        achievementListRecorder.cardsClickable[achievementListRecorder.cardsName.IndexOf("TenGemsPerDayCard")] = false;

        GemController.Add10Gem();

        CardClick();
    }

    private void CardClick() // 卡片的通用行为
    {
        // 粒子效果
        if (particles.activeSelf)
        {
            particles.SetActive(false);
        }
        particles.SetActive(true);

        Global.AchievementSound.Play();
        achievementListRecorder.newCardNumber -= 1;
        SaveAchievementList();
    }

    #endregion 点击成就卡片领取奖励的动作

    // *************************重置成就，目前只用于Debug
    public void ResetAchievements()
    {
        PlayerPrefs.DeleteKey("AchievementListRecorder");
        PlayerPrefs.DeleteKey("ChoosenPlayer");
        PlayerPrefs.DeleteKey("CharactorsActive");
        PlayerPrefs.DeleteKey("GemNumber");
    }
}