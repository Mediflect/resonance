using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CycleTracker : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Awake()
    {
        App.Request(OnAppExists);
        text.SetText("");
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycleStart;
        App.Cycle.Cycle9 += OnCycle9;
        App.Cycle.Cycle8 += OnCycle8;
        App.Cycle.Cycle7 += OnCycle7;
        App.Cycle.Cycle6 += OnCycle6;
        App.Cycle.Cycle5 += OnCycle5;
        App.Cycle.Cycle4 += OnCycle4;
        App.Cycle.Cycle3 += OnCycle3;
        App.Cycle.Cycle2 += OnCycle2;
        App.Cycle.Cycle1 += OnCycle1;
        App.Cycle.CycleStopped += OnCycleStopped;
    }

    private void OnDestroy()
    {
        if (App.Exists)
        {
            App.Cycle.CycleStarted -= OnCycleStart;
            App.Cycle.Cycle9 -= OnCycle9;
            App.Cycle.Cycle8 -= OnCycle8;
            App.Cycle.Cycle7 -= OnCycle7;
            App.Cycle.Cycle6 -= OnCycle6;
            App.Cycle.Cycle5 -= OnCycle5;
            App.Cycle.Cycle4 -= OnCycle4;
            App.Cycle.Cycle3 -= OnCycle3;
            App.Cycle.Cycle2 -= OnCycle2;
            App.Cycle.Cycle1 -= OnCycle1;
            App.Cycle.CycleStopped -= OnCycleStopped;
        }
    }

    private void OnCycleStart() => text.SetText("10");
    private void OnCycle9() => text.SetText("9");
    private void OnCycle8() => text.SetText("8");
    private void OnCycle7() => text.SetText("7");
    private void OnCycle6() => text.SetText("6");
    private void OnCycle5() => text.SetText("5");
    private void OnCycle4() => text.SetText("4");
    private void OnCycle3() => text.SetText("3");
    private void OnCycle2() => text.SetText("2");
    private void OnCycle1() => text.SetText("1");
    private void OnCycleStopped() => text.gameObject.SetActive(false);

}
