using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerApeManController : PlayerController
{
    private float newJumpForce; // 跳跃推力

    private new void Awake()
    {
        newJumpForce = 900.0f;

        base.Awake();
        jumpForce = newJumpForce;
    }
}
