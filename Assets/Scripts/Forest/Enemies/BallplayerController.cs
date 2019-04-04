using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallplayerController : MonoBehaviour
{
    public AudioSource waveSound;

    private void SoundWave()
    {
        waveSound.Play();
    }
}
