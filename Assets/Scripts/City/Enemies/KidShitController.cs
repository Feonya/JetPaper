using UnityEngine;

public class KidShitController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        gameObject.SetActive(false);

        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            gameObject.transform.SetAsFirstSibling(); // 设为视图列表父物体下的第一个物体
            gameObject.SetActive(false);
            playerController.EatShit();
        }
    }
}