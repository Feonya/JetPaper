using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlysController : CatsController
{
    private float diffPosY;

    private new void Start()
    {
        base.Start();

        diffPosY = catTransform.position.y - playerTransform.position.y;
    }

    // 跟随玩家
    protected override void FollowPlayer()
    {
        catTransform.position = new Vector3(playerTransform.position.x - 1.0f, playerTransform.position.y + diffPosY, 0.0f);
    }
}
