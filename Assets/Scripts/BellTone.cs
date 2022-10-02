using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellTone : MonoBehaviour
{
    public AudioSource ambience;
    public AudioSource bellSound;
    public AudioSource bellAnticipationSound;

    private void Awake()
    {
        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycleStarted;
        App.Cycle.Cycle2 += OnCycle2;
    }

    private void OnCycleStarted()
    {
        if (!ambience.isPlaying)
        {
            ambience.Play();
        }
        bellSound.Play();
    }

    private void OnCycle2()
    {
        // bellAnticipationSound.Play();
    }
}
