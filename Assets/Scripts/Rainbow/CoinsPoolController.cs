using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinsPoolController : MonoBehaviour
{
    private Transform coinsPoolTransform;
    public LayerMask coinsLayer;
    
    private float[] current12TilesPositionX; // 保存每个cloudsign后的12个tiles的世界坐标X
    private float[] current12TilesPositionY; // 保存每个cloudsign后的12个tiles的世界坐标X

    private int coinGenerateNumber = 1; // 每一个coloudsign增加一个金币
    private int GenerateTimes = 25; // 最大生成次数25次

    private void Start()
    {
        coinsPoolTransform = transform;

        current12TilesPositionX = new float[12];
        current12TilesPositionY = new float[12];
}

    public void Get1TileOfCurrent12TilesPosition(int i, Vector3 tPos) // 没生成一个tile调用一次
    {
        current12TilesPositionX[i % 12] = tPos.x;
        current12TilesPositionY[i % 12] = tPos.y;
    }

    public void SetCoins() // 每生成12个tile调用一次
    {
        if (GenerateTimes > 0)
        {
            for (int n = coinGenerateNumber; n > 0; n--) // n代表本次12个tiles里面要生成的金币数量
            {
                Transform c = GetOneCoinInPool(); // 从池中获得第一个coin并置于末尾

                int tileIndex = Random.Range(0, 12);
                float rx = current12TilesPositionX[tileIndex] + (Random.Range(0, 2)) / 2.0f;
                float ry = current12TilesPositionY[tileIndex] + (Random.Range(1, 10)) + (Random.Range(0, 2)) / 2.0f;
                c.position = new Vector2(rx, ry);

                // 如果金币重叠，则循环
                while (c.GetComponent<Collider2D>().IsTouchingLayers(coinsLayer))
                {
                    tileIndex = Random.Range(0, 12);
                    rx = current12TilesPositionX[tileIndex] + (Random.Range(0, 2)) / 2.0f;
                    ry = current12TilesPositionY[tileIndex] + (Random.Range(1, 10)) + (Random.Range(0, 2)) / 2.0f;
                    c.position = new Vector2(rx, ry);
                }
            }

            // 满足条件后，保持每个cloudsign后金币生成数量恒定
            if (coinGenerateNumber < 10)
            {
                coinGenerateNumber += 1;
            }

            GenerateTimes -= 1;
        }
    }

    private Transform GetOneCoinInPool()
    {
        if (coinsPoolTransform.childCount != 0)
        {
            Transform c = coinsPoolTransform.GetChild(0);
            c.SetAsLastSibling();
            return c;
        }
        else
        {
            return null;
        }
    }
}
