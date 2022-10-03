using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Medi;

public class ToCourtyardLasers : MonoBehaviour
{
    public List<LaserEmitter> allLasers = new List<LaserEmitter>();

    private void Awake()
    {
        App.Request(OnAppExists);
        SetLasersActive(true);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycleStart;
        App.Cycle.Cycle9 += OnCycle9;
    }

    private void OnCycleStart()
    {
        SetLasersActive(false);
    }

    private void OnCycle9()
    {
        SetLasersActive(true);
    }

    private void SetLasersActive(bool active)
    {
        foreach (LaserEmitter emitter in allLasers)
        {
            emitter.SetLaserActive(active);
        }
    }

    [ContextMenu("Gather Lasers")]
    private void GatherLasers()
    {
        LaserEmitter[] emitters = transform.GetComponentsInChildren<LaserEmitter>();
        allLasers.Clear();
        for (int i = 0; i < emitters.Length; ++i)
        {
            allLasers.Add(emitters[i]);
        }
    }
}
