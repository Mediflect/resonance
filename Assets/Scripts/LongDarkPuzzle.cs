using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDarkPuzzle : MonoBehaviour
{
    public GameObject lightObj;

    private void Awake()
    {
        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycleStarted;
        App.Cycle.Cycle9 += OnCycle8;
    }

    private void OnDestroy()
    {
        if (App.Exists)
        {
            App.Cycle.CycleStarted -= OnCycleStarted;
            App.Cycle.Cycle8 -= OnCycle8;
        }
    }

    private void OnCycleStarted()
    {
        lightObj.SetActive(true);
    }

    private void OnCycle8()
    {
        lightObj.SetActive(false);
    }
}
