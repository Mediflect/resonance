using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Things that should exist for the lifetime of the app
/// </summary>
public class App : MonoBehaviour
{
    private static App Instance = null;

    public static Cycle Cycle => Instance.cycle;
    public Cycle cycle;

    public static Player Player => Instance.player;
    public Player player;

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
            Debug.LogError("Multiple apps exist!!!");
        }
        Instance = this;
        foreach(Action existsCallback in requestsForApp)
        {
            existsCallback?.Invoke();
        }
        requestsForApp.Clear();

        cycle.CycleStarted += () => Debug.Log("10");
        cycle.Cycle9 += () => Debug.Log("9");
        cycle.Cycle8 += () => Debug.Log("8");
        cycle.Cycle7 += () => Debug.Log("7");
        cycle.Cycle6 += () => Debug.Log("6");
        cycle.Cycle5 += () => Debug.Log("5");
        cycle.Cycle4 += () => Debug.Log("4");
        cycle.Cycle3 += () => Debug.Log("3");
        cycle.Cycle2 += () => Debug.Log("2");
        cycle.Cycle1 += () => Debug.Log("1");

    }
}
