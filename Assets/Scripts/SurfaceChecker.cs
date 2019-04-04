using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceChecker : MonoBehaviour
{
    private Vector2 originPoint;
    private Vector2 direction;
    private float distance;
    public LayerMask whatIsSurface;

    private void Start()
    {
        originPoint.y = 10.0f;
        direction = Vector2.down;
        distance = 20.0f;
    }

    public Vector2 CheckSurface(float x)
    {
        originPoint.x = x;

        RaycastHit2D rayHit = Physics2D.Raycast(originPoint, direction, distance, whatIsSurface);

        return rayHit.point;
    }
}
