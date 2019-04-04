using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinderController : MonoBehaviour
{
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
