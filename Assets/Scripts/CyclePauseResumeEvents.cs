using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CyclePauseResumeEvents : MonoBehaviour
{
    public UnityEvent CyclePaused;
    public UnityEvent CycleResumed;

    private void Awake()
    {
        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        App.Cycle.CyclePaused += OnCyclePaused;
        App.Cycle.CycleResumed += OnCycleResumed;
    }

    private void OnCyclePaused() => CyclePaused.Invoke();
    private void OnCycleResumed() => CycleResumed.Invoke();
}
