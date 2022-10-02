using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchRandomizer : MonoBehaviour
{
    public AudioSource audioSource;
    public float variance = 0.1f;

    public void Play()
    {
        audioSource.pitch = Random.Range(1 - variance, 1 + variance);
        audioSource.Play();
    }
}
