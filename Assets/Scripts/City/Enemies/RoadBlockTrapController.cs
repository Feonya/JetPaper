using UnityEngine;

public class RoadBlockTrapController : MonoBehaviour
{
    private Transform roadBlockTrapTransform;

    private void Start()
    {
        roadBlockTrapTransform = transform;
    }

    private void FixedUpdate()
    {
        if (roadBlockTrapTransform.position.x >= 262.66) // 262.66表示能碰到最后一个金币炸弹的位置
        {
            Destroy(gameObject);
        }
    }
}