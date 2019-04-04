using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerYellowHatBoyController : PlayerController
{
    private float newMaxSpeed;
    private float newSensitivity;
    private float newPowerValueIncrement;
    private float newPowerBarAmountIncrement;

    private new void Awake()
    {
        newMaxSpeed = 7.0f;
        newSensitivity = 60.0f;
        newPowerValueIncrement = 120.0f / 100.0f;
        newPowerBarAmountIncrement = (120.0f / 100.0f) * 0.005f;

        base.Awake();
        maxSpeed = newMaxSpeed;
        sensitivity = newSensitivity;
        powerValueIncrement = newPowerValueIncrement;
        powerBarAmountIncrement = newPowerBarAmountIncrement;
    }
}
