using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyController : KidController
{
    protected override void Shit()
    {
        if (player.name != "PlayerApeMan")
        {
            if (onShit)
            {
                if (!animator.GetBool("shitting"))
                {
                    animator.SetBool("shitting", true);
                    animator.SetBool("moving", false);

                    shit.SetActive(true);
                    shitTransform.position = kidTransform.position - new Vector3(-0.16f, 0.24f, 0.0f);
                    shitTransform.SetAsLastSibling(); // 拉出来的屎放在父物体下的最后

                    shitSound.Play();
                }
            }
        }
    }
}
