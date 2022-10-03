using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class KingKiller : MonoBehaviour
{
    public LaserEmitter emitter;
    public bool isPowered = false;
    public ToggleSwitch toggleSwitch;

    private void Awake()
    {
        App.Request(OnAppExists);
        emitter.SetLaserActive(false);
        toggleSwitch.TurnedOn.AddListener(() => isPowered = true);
        toggleSwitch.TurnedOff.AddListener(() => isPowered = false);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycleStarted;
        App.Cycle.Cycle8 += OnCycle8;
        App.Cycle.CycleStopped += OnCycleStopped;
    }

    private void OnCycleStarted()
    {
        if (isPowered)
        {
            emitter.SetLaserActive(true);
        }
    }

    private void OnCycle8()
    {
        if (emitter.isLaserActive)
        {
            emitter.SetLaserActive(false);
            if (toggleSwitch.isOn)
            {
                toggleSwitch.Toggle();
            }
        }

    }

    private void OnCycleStopped()
    {
        StartCoroutine(RunCycleStopResponse());
    }

    private IEnumerator RunCycleStopResponse()
    {
        yield return YieldInstructionCache.WaitForSeconds(5f);
        emitter.SetLaserActive(false);
    }
}
