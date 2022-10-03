using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public AudioSource ambience;
    public float savedTime = 0f;

    private void Awake()
    {
        App.Request(OnAppExists);
    }
    private void OnAppExists()
    {
        App.Cycle.CyclePaused += OnCyclePaused;
        App.Cycle.CycleResumed += OnCycleResumed;
        App.Cycle.CycleStopped += OnCycleStopped;
    }

    private void OnDestroy()
    {
        if (App.Exists)
        {
            App.Cycle.CyclePaused -= OnCyclePaused;
            App.Cycle.CycleResumed -= OnCycleResumed;
            App.Cycle.CycleStopped -= OnCycleStopped;
        }
    }

    private void OnCyclePaused()
    {
        savedTime = ambience.time;
        ambience.Stop();
    }

    private void OnCycleResumed()
    {
        ambience.Play();
        ambience.time = savedTime;
    }

    private void OnCycleStopped()
    {
        ambience.Stop();
    }
}
