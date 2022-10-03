using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CyclePauseResumeEvents : MonoBehaviour
{
    public UnityEvent CyclePaused;
    public UnityEvent CycleResumed;
    public UnityEvent CycleStopped;

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

    private void OnCyclePaused() => CyclePaused.Invoke();
    private void OnCycleResumed() => CycleResumed.Invoke();
    private void OnCycleStopped() => CycleStopped.Invoke();
}
