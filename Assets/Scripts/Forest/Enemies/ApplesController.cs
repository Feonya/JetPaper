using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplesController : MonoBehaviour
{
    private Transform playerTransform;

    private Transform applesTransform;

    public static bool alive;

    private void Start()
    {
        playerTransform = PlayerChooser.ChoosePlayer().transform;

        applesTransform = transform;

        alive = false;
    }

    private void FixedUpdate()
    {
        CheckPlayerInRange();
    }

    // 检测玩家相对位置
    private void CheckPlayerInRange()
    {
        if (applesTransform.position.x - playerTransform.position.x < -18.0f) // 如果玩家在后方18个单位之后
        {
            if (alive)
            {
                alive = false;
            }
        }
        else if (applesTransform.position.x - playerTransform.position.x < 6.0f) // 如果玩家进入前方6个单位
        {
            if (!alive)
            {
                alive = true;
            }
        }
    }
}
