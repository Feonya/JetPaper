using UnityEngine;

public class CoinBombController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    public GameObject plane;
    private PlaneController planeController;

    //public GameObject explosion;
    public AudioSource explodeSound;

    private Animator animator;

    //public GameObject roadBlockTrap;
    //private RoadBlockTrapController roadBlockTrapController;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        playerController = player.GetComponent<PlayerController>();
        planeController = plane.GetComponent<PlaneController>();

        //explosion.SetActive(false);

        animator = gameObject.GetComponent<Animator>();

        //roadBlockTrapController = roadBlockTrap.GetComponent<RoadBlockTrapController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.Dead();

            explodeSound.Play();
            animator.SetBool("exploding", true); // 切换爆炸动画
        }
        else if (collision.CompareTag("Plane"))
        {
            //playerController.Dead();
            planeController.Disappear();

            explodeSound.Play();
            animator.SetBool("exploding", true); // 切换爆炸动画
        }
        else if (collision.CompareTag("RoadBlockTrap"))
        {
            explodeSound.Play();
            animator.SetBool("exploding", true); // 切换爆炸动画
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}