using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RainbowRoadGenerator : MonoBehaviour
{
    public Transform cloudSignsPool;
    private int cloudSignInPoolNumber;

    public Transform finishCloudSignTransform;

    private Tile currentRainbowRoadTile;
    private Tile previousRainbowRoadTile;

    private Vector3Int currentPostion;
    private Vector3Int previousPostion;

    public Tile flatRainbowRoadTile;
    public Tile upwardRainbowRoadTile;
    public Tile downwardRainbowRoadTile;

    private Tilemap rainbowRoadTilemap;

    private int tileGenerationNumber;

    public CoinsPoolController coinsPoolController;

    private void Start()
    {
        cloudSignInPoolNumber = 21;

        previousPostion = new Vector3Int(0, -1, 0);

        rainbowRoadTilemap = GetComponent<Tilemap>();

        previousRainbowRoadTile = rainbowRoadTilemap.GetTile<Tile>(previousPostion); // 游戏启动先获得起点处固定的tile

        tileGenerationNumber = 0;

        // 生成随机tile
        for (int i = 0; i < 315; i++)
        {
            PutTile();
            tileGenerationNumber += 1;

            Vector3 tPos = rainbowRoadTilemap.CellToWorld(currentPostion); // 获得当前tile的世界坐标

            // 每第12个生成的tile判定一次
            if (tileGenerationNumber % 12 == 0)
            {
                if (cloudSignInPoolNumber > 0) // 如果cloudsign池中还有东西
                {
                    Transform randC = cloudSignsPool.GetChild(Random.Range(0, cloudSignInPoolNumber)); // 随机获取一个cloudsign
                    cloudSignInPoolNumber -= 1;
                    randC.SetParent(transform);
                    randC.transform.position = tPos + new Vector3(1.0f, 3.5f, 0.0f);
                }

                coinsPoolController.SetCoins();

                PutFinishCloudSign(tileGenerationNumber, tPos);
            }

            coinsPoolController.Get1TileOfCurrent12TilesPosition(i, tPos);
        }
    }

    private void PutFinishCloudSign(int n, Vector3 tPos)
    {
        if (n == 300)
        {
            finishCloudSignTransform.position = new Vector3(finishCloudSignTransform.position.x, tPos.y + 3.5f);
        }
    }

    private void PutTile()
    {
        currentRainbowRoadTile = RandomGenerateATile();
        currentPostion = GetCurrentTilePosition();

        rainbowRoadTilemap.SetTile(currentPostion, currentRainbowRoadTile);

        previousRainbowRoadTile = currentRainbowRoadTile;
        previousPostion = currentPostion;
    }

    private Vector3Int GetCurrentTilePosition()
    {
        Vector3Int currentPos = Vector3Int.zero;

        if (previousRainbowRoadTile == flatRainbowRoadTile)
        {
            if (currentRainbowRoadTile == downwardRainbowRoadTile)
            {
                currentPos = previousPostion + new Vector3Int(1, -1, 0);
            }
            else
            {
                currentPos = previousPostion + new Vector3Int(1, 0, 0);
            }
        }
        else if (previousRainbowRoadTile == upwardRainbowRoadTile)
        {
            if (currentRainbowRoadTile == downwardRainbowRoadTile)
            {
                currentPos = previousPostion + new Vector3Int(1, 0, 0);
            }
            else
            {
                currentPos = previousPostion + new Vector3Int(1, 1, 0);
            }
        }
        else if (previousRainbowRoadTile == downwardRainbowRoadTile)
        {
            if (currentRainbowRoadTile == downwardRainbowRoadTile)
            {
                currentPos = previousPostion + new Vector3Int(1, -1, 0);
            }
            else
            {
                currentPos = previousPostion + new Vector3Int(1, 0, 0);
            }
        }

        return currentPos;
    }

    private Tile RandomGenerateATile()
    {
        int randNumber = Random.Range(0, 3); // 随机0、1、2三个数

        if (randNumber == 0)
        {
            return flatRainbowRoadTile;
        }
        else if (randNumber == 1)
        {
            return upwardRainbowRoadTile;
        }
        else
        {
            return downwardRainbowRoadTile;
        }
    }
}
