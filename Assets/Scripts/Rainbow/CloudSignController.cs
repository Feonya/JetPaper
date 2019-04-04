using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSignController : MonoBehaviour
{
    private float velocityY;
    private int direction; // 1上，-1下

    private void Start()
    {
        velocityY = 0.01f;

        StartCoroutine(MoveDown());
    }

    private void FixedUpdate()
    {
        if (direction == 1)
        {
            transform.position += new Vector3(0.0f, velocityY, 0.0f);
        }
        else // direction == -1
        {
            transform.position -= new Vector3(0.0f, velocityY, 0.0f);
        }
    }

    IEnumerator MoveUp()
    {
        direction = 1;

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        direction = -1;

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(MoveUp());
    }
}
