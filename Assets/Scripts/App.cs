using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Things that should exist for the lifetime of the app
/// </summary>
public class App : MonoBehaviour
{
    private static App Instance = null;
    public static bool Exists => Instance != null;

    public static Cycle Cycle => Instance.cycle;
    public Cycle cycle;

    public static Volume TimestopEffectVolume => Instance.timestopEffectVolume;
    public Volume timestopEffectVolume;

    public static Volume PreTimestopEffectVolume => Instance.preTimestopEffectVolume;
    public Volume preTimestopEffectVolume;

    public static Volume BlackFadeVolume => Instance.blackFadeVolume;
    public Volume blackFadeVolume;

    private static List<Action> requestsForApp = new List<Action>();

    public static void Request(Action onAppExists)
    {
        if (Instance != null)
        {
            onAppExists?.Invoke();
        }
        else
        {
            requestsForApp.Add(onAppExists);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        foreach(Action existsCallback in requestsForApp)
        {
            existsCallback?.Invoke();
        }
        requestsForApp.Clear();

        cycle.CycleStarted += () => Debug.Log("cycle: 10");
        cycle.Cycle9 += () => Debug.Log("cycle: 9");
        cycle.Cycle8 += () => Debug.Log("cycle: 8");
        cycle.Cycle7 += () => Debug.Log("cycle: 7");
        cycle.Cycle6 += () => Debug.Log("cycle: 6");
        cycle.Cycle5 += () => Debug.Log("cycle: 5");
        cycle.Cycle4 += () => Debug.Log("cycle: 4");
        cycle.Cycle3 += () => Debug.Log("cycle: 3");
        cycle.Cycle2 += () => Debug.Log("cycle: 2");
        cycle.Cycle1 += () => Debug.Log("cycle: 1");

    }
}
