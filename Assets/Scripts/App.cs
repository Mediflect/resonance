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
        cycle.CycleStarted += () => Debug.Log("cycle");
    }
}
