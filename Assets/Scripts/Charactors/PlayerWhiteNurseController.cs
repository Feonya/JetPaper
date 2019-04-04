using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWhiteNurseController : PlayerController {

    private float newMaxSpeed;
    private float newPowerValueIncrement;
    private float newPowerValue;

    private float newGroundCheckRadius;

    private new void Awake()
    {
        newMaxSpeed = 3.5f;
        newPowerValueIncrement = 120.0f / 100.0f;
        newPowerValue = 0.0f;

        newGroundCheckRadius = 0.1f;

        base.Awake();

        maxSpeed = newMaxSpeed;
        powerValueIncrement = newPowerValueIncrement;
        powerValue = newPowerValue;

        groundCheckRadius = newGroundCheckRadius;
    }

    public override void Love()
    {
        //Debug.Log("do nothing");
    }

    public override void EatShit()
    {
        //Debug.Log("do nothing");
    }
}
