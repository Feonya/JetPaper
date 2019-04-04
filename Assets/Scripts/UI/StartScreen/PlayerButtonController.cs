/// 本脚本PlayerPrefs用了一个键："ChoosenPlayer"

using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonController : MonoBehaviour
{
    public GameObject charactorSelectCanvas;
    private string choosenPlayer;

    //private Image image;
    private Animator animator;

    // --------------------------------------每添加一个新角色，这里就要增加一个变量
    #region 待更换定义

    public RuntimeAnimatorController greenHatBoyAnimatorController;
    public RuntimeAnimatorController apeManAnimatorController;
    public RuntimeAnimatorController yellowHatBoyAnimatorController;
    public RuntimeAnimatorController whiteNurseAnimatorController;
    public RuntimeAnimatorController coloredEggAnimatorController;

    #endregion 待更换定义

    private void Awake()
    {
        //image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        InitChoosenPlayer();
    }

    private void InitChoosenPlayer()
    {
        choosenPlayer = PlayerPrefs.GetString("ChoosenPlayer");

        if (choosenPlayer == "")
        {
            choosenPlayer = "GreenHatBoy";
            PlayerPrefs.SetString("ChoosenPlayer", choosenPlayer);
        }
        else
        {
            // -----------------------------------每添加一个新角色，这里就要增加一个选择
            switch (choosenPlayer)
            {
                case "GreenHatBoy":
                    animator.runtimeAnimatorController = greenHatBoyAnimatorController;
                    break;

                case "ApeMan":
                    animator.runtimeAnimatorController = apeManAnimatorController;
                    break;

                case "YellowHatBoy":
                    animator.runtimeAnimatorController = yellowHatBoyAnimatorController;
                    break;

                case "WhiteNurse":
                    animator.runtimeAnimatorController = whiteNurseAnimatorController;
                    break;

                case "ColoredEgg":
                    animator.runtimeAnimatorController = coloredEggAnimatorController;
                    break;
            }
        }
    }

    public void OnPlayerButtonClick()
    {
        charactorSelectCanvas.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}