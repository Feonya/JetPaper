using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorWordsController : MonoBehaviour
{
    private Text wordsText;
    private GameObject nextSign;

    public int wordsNumber;

    public TutorPlayerController tutorPlayerController;
    public TutorPlaneController tutorPlaneController;

    public AudioSource nextWordsSound;

    private GameObject newManCard;

    private void Start()
    {
        wordsText = GetComponentInChildren<Text>();
        wordsText.text = "您好!我叫做绿帽君，欢迎来到《喷气纸机》的新手教学。";

        nextSign = GameObject.Find("NextSign");

        wordsNumber = 1; 
    }

    public void OnWordsClick()
    {
        switch (wordsNumber)
        {
            case 1:
                wordsText.text = "这个游戏是一款颇有挑战性，却趣味十足的休闲类游戏。";
                wordsNumber = 2;
                nextWordsSound.Play();
                break;

            case 2:
                wordsText.text = "游戏的基本玩法是在前进中保持飞机别落地，并躲避重重障碍，最终到达终点。";
                wordsNumber = 3;
                nextWordsSound.Play();
                break;

            case 3:
                if (nextSign.activeSelf)
                {
                    nextSign.SetActive(false);
                    tutorPlayerController.canMove = true;
                    wordsText.text = "下面请左右倾斜手机，控制我移动到右侧的终点线处，然后再返回起点线处。";
                    nextWordsSound.Play();
                }
                break;

            case 4:
                if (!nextSign.activeSelf)
                {
                    nextSign.SetActive(true);
                    tutorPlayerController.canMove = false;
                    tutorPlayerController.speed = 0.0f;
                    tutorPlayerController.body.velocity = Vector2.zero;
                    wordsText.text = "棒棒哒~~，熟练控制手机的倾斜，是及时和准确移动的重要操作。";
                    wordsNumber = 5;
                    nextWordsSound.Play();
                }
                break;

            case 5:
                if (nextSign.activeSelf)
                {
                    nextSign.SetActive(false);
                    tutorPlayerController.EnableJumpButton();
                    wordsText.text = "下面请点击左下角的方块按钮。";
                    nextWordsSound.Play();
                }
                break;

            case 6:
                if (!nextSign.activeSelf)
                {
                    nextSign.SetActive(true);
                    wordsText.text = "如您所见，这个按钮控制跳跃。";
                    wordsNumber = 7;
                    nextWordsSound.Play();
                }
                break;

            case 7:
                if (nextSign.activeSelf)
                {
                    nextSign.SetActive(false);
                    tutorPlayerController.EnableBlowButton();
                    wordsText.text = "接着，请点击右下角的方块按钮。";
                    nextWordsSound.Play();
                }
                break;

            case 8:
                if (!nextSign.activeSelf)
                {
                    nextSign.SetActive(true);
                    wordsText.text = "没错，这个按钮控制吹气，长按还可以聚气哦~~";
                    wordsNumber = 9;
                }
                break;

            case 9:
                wordsText.text = "注意左上角的蓝色柱状物（力量槽），它可以显示聚气的力度大小。";
                wordsNumber = 10;
                nextWordsSound.Play();
                break;

            case 10:
                if (nextSign.activeSelf)
                {
                    nextSign.SetActive(false);
                    tutorPlayerController.EnableBlowButton();
                    wordsText.text = "请长按吹气按钮，待力量槽加满后松开。";
                    nextWordsSound.Play();
                }
                break;

            case 11:
                if (!nextSign.activeSelf)
                {
                    nextSign.SetActive(true);
                    wordsText.text = "可以看到，聚力越多，吹出的气体也越大越威猛！但是在跳跃时不可吹气哦。";
                    wordsNumber = 12;
                }
                break;

            case 12:
                nextSign.SetActive(true);
                wordsText.text = "接下来请注意下方的三行绿色数据。";
                wordsNumber = 13;
                nextWordsSound.Play();
                break;

            case 13:
                nextSign.SetActive(true);
                wordsText.text = "分数是对您得分的记录，人物的不断前进以及获取金币都可以为您加分。";
                wordsNumber = 14;
                nextWordsSound.Play();
                break;

            case 14:
                nextSign.SetActive(true);
                wordsText.text = "路程是对我前进距离的记录，如果后退会减分的哦，游戏会在路程100处结束。";
                wordsNumber = 15;
                nextWordsSound.Play();
                break;

            case 15:
                nextSign.SetActive(true);
                wordsText.text = "风力是随机变化的，它会影响飞机的飞行速度，要注意突然而来的风力改变。";
                wordsNumber = 16;
                nextWordsSound.Play();
                break;

            case 16:
                nextSign.SetActive(true);
                wordsText.text = "好了，飞机马上就要启航了，注意飞机在默认风力下的飞行轨迹。";
                wordsNumber = 17;
                nextWordsSound.Play();
                break;

            case 17:
                if (nextSign.activeSelf)
                {
                    nextSign.SetActive(false);
                    tutorPlaneController.EnablePhysicsSimulate();
                    tutorPlaneController.OutOfForcedIdleState();
                    wordsText.text = "飞机飞行中。。。";
                    nextWordsSound.Play();
                }
                break;

            case 18:
                if (!nextSign.activeSelf)
                {
                    nextSign.SetActive(true);
                    wordsText.text = "飞机快落地了，让我过去将其吹起来吧！";
                    wordsNumber = 19;
                    nextWordsSound.Play();
                }
                break;

            case 19:
                gameObject.SetActive(false);
                wordsText.text = "非常好！这样飞机就又飞起来啦~~";
                tutorPlayerController.canMove = true;
                tutorPlayerController.free = true;
                tutorPlayerController.EnableBlowButton();
                tutorPlayerController.EnableJumpButton();
                wordsNumber = 20;
                break;

            case 20:
                if (nextSign.activeSelf)
                {
                    nextSign.SetActive(false);
                    wordsText.text = "恭喜您完成了新手教学！祝您在《喷气纸机》里玩儿的开心！(<color=\"#f9938a\">按右上角按钮返回</color>)";
                    nextWordsSound.Play();

                    AchievementsAndHighscoresController.achievementListRecorder.EnableCard("ApeManCard");
                }
                break;
        }
    }

    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("StartScreen");
    }
}