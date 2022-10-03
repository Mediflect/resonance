using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicidePuzzle : MonoBehaviour
{
    public ToggleSwitch toggleSwitch;
    public int cyclesBeforeClose = 0;

    private void Awake()
    {
        App.Request(OnAppExists);
        toggleSwitch.TurnedOn.AddListener(OnSwitchTurnedOn);
        toggleSwitch.TurnedOff.AddListener(OnSwitchTurnedOff);
    }

    private void OnAppExists()
    {
        App.Cycle.AnyCycle += OnAnyCycle;
    }

    private void OnDestroy()
    {
        if (App.Exists)
        {
            App.Cycle.AnyCycle -= OnAnyCycle;
        }
    }

    private void OnAnyCycle()
    {
        if (cyclesBeforeClose > 0)
        {
            --cyclesBeforeClose ;
            if (cyclesBeforeClose == 0 && toggleSwitch.isOn)
            {
                toggleSwitch.Toggle();
            }
        }
    }

    private void OnSwitchTurnedOn()
    {
        cyclesBeforeClose = 2;
    }

    private void OnSwitchTurnedOff()
    {
        cyclesBeforeClose = 0;
    }
}
