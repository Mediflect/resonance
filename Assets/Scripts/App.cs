using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    private static App Instance = null;
    public static Cycle Cycle => Instance.cycle;

    public Cycle cycle;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple apps exist!!!");
        }
        Instance = this;

        cycle.CycleStarted += () => Debug.Log("cycle");
    }
}
