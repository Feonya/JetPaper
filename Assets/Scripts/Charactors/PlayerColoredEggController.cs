using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColoredEggController : PlayerController
{
    private float newGroundCheckRadius;

    private new void Awake()
    {
        base.Awake();

        newGroundCheckRadius = 0.15f;
        groundCheckRadius = newGroundCheckRadius;
    }

    // 执行跳跃并进入跳跃状态
    public override void Jump()
    {
        if (canJump)
        {
            if (onGround)
            {
                body.AddForce(new Vector2(0.0f, jumpForce * 0.8f));

            }
            else
            {
                body.AddForce(new Vector2(0.0f, jumpForce + 60.0f));
            }
            jumpSound.Play();
        }
    }

    public override void EnableGravity()
    {
        if (body.gravityScale != 2.0f)
        {
            body.gravityScale = 2.0f;
        }
    }

    protected override void CheckAction()
    {
        // 死亡
        if (state == States.Dead)
        {
            if (onGround)
            {
                speed = 0.0f;
                body.velocity = new Vector2(0.0f, body.velocity.y);
            }

            // 使气体消失
            if (air.activeSelf)
            {
                DisappearAir();
            }

            // 退出其他状态
            if (onForcedIdleState)
            {
                onForcedIdleState = false;
            }
            if (onLovedState)
            {
                onLovedState = false;
            }
            if (onTrippedState)
            {
                onTrippedState = false;
            }
            if (onShittedState)
            {
                onShittedState = false;
            }
            if (onInhaleState)
            {
                onInhaleState = false;
            }
            if (onBlowState)
            {
                onBlowState = false;
            }

            DisableBlowButton();
            DisableJumpButton();

            // 激活广告展示倒计时
            if (!deadSound.isPlaying)
            {
                if (!rebirthConfirm.activeSelf)
                {
                    rebirthConfirm.SetActive(true);
                }
            }
        }
        // 被恶心
        else if (state == States.Shitted)
        {
            if (onGround)
            {
                speed = 0.0f;
            }

            // 使气体消失
            if (air.activeSelf)
            {
                DisappearAir();
            }

            if (onInhaleState)
            {
                onInhaleState = false;
            }
            if (onBlowState)
            {
                onBlowState = false;
            }

            DisableBlowButton();
            DisableJumpButton();
        }
        // 摔倒
        else if (state == States.Tripped)
        {
            if (onGround)
            {
                if (speed >= 0.0f)
                {
                    speed = 2.0f;
                }
                else
                {
                    speed = -2.0f;
                    spriteRenderer.flipX = true;
                }
            }

            // 使气体消失
            if (air.activeSelf)
            {
                DisappearAir();
            }

            if (onInhaleState)
            {
                onInhaleState = false;
            }
            if (onBlowState)
            {
                onBlowState = false;
            }

            DisableBlowButton();
            DisableJumpButton();
        }
        // 被迷住
        else if (state == States.Loved)
        {
            if (onGround)
            {
                speed = 0.0f;
            }

            // 使气体消失
            if (air.activeSelf)
            {
                DisappearAir();
            }

            if (onInhaleState)
            {
                onInhaleState = false;
            }
            if (onBlowState)
            {
                onBlowState = false;
            }

            DisableBlowButton();
            DisableJumpButton();
        }
        // 强制原地
        else if (state == States.ForcedIdle)
        {
            speed = 0.0f;
            body.velocity = new Vector2(0.0f, 0.0f);

            if (onInhaleState)
            {
                onInhaleState = false;
            }
            if (onBlowState)
            {
                onBlowState = false;
            }

            // 使气体消失
            if (air.activeSelf)
            {
                DisappearAir();
            }
        }
        // 吸气
        else if (state == States.Inhale)
        {
            canJump = false;
            speed = 0.0f;
            hudCanvasController.IncreasePower();

            DisableJumpButton();
        }
        // 吹气
        else if (state == States.Blow)
        {
            speed = 0.0f;
            animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
            // 判断动画是否结束... (normalizedTime是一个0~1的浮点数，0代表动画的开头，1表示动画的结尾)
            if (animatorInfo.IsName("PlayerBlow") && animatorInfo.normalizedTime >= 1.0f)
            {
                onBlowState = false;
                canJump = true;
            }

            DisableBlowButton();
            DisableJumpButton();
        }
        // 跳跃
        else if (state == States.Jump)
        {
            GetAccX();

            if (body.velocity.y > 0.0f)
            {
                if (canJump)
                {
                    canJump = false;
                }
            }
            else
            {
                if (!canJump)
                {
                    canJump = true;
                }

                EnableJumpButton();
            }

                DisableBlowButton();
        }
        // 移动
        else if (state == States.Move)
        {
            GetAccX();

            if (body.velocity.y <= 0.0f)
            {
                if (!canJump)
                {
                    canJump = true;
                }

                EnableBlowButton();
                EnableJumpButton();
            }
        }
        // 原地
        else if (state == States.Idle)
        {
            GetAccX();

            if (body.velocity.y <= 0.0f)
            {
                if (!canJump)
                {
                    canJump = true;
                }

                EnableBlowButton();
                EnableJumpButton();
            }
        }
    }
}
