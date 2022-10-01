using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftersSwitch : MonoBehaviour
{
    public ToggleSwitch toggleSwitch;

    private void Awake()
    {
        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycleStarted;
    }

    private void OnCycleStarted()
    {
        if (toggleSwitch.isOn)
        {
            toggleSwitch.Toggle();
        }
    }
}
