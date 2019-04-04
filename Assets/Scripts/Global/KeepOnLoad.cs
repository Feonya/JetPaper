using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnLoad : MonoBehaviour
{
    private static GameObject TheFirstGameObject;

    private void Start()
    {
        if (TheFirstGameObject == null)
        {
            TheFirstGameObject = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
