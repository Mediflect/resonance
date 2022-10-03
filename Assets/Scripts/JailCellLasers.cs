using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Medi;

public class JailCellLasers : MonoBehaviour
{
    public bool isPowered = true;
    public List<LaserEmitter> allLasers = new List<LaserEmitter>();

    public void SetPowered(bool powered)
    {
        isPowered = powered;
        // Debug.Log("Cell lasers powered changed to " + powered);
    }

    private void Awake()
    {
        App.Request(OnAppExists);
        SetLasersActive(false);
    }

    private void OnAppExists()
    {
        App.Cycle.Cycle3 += OnCycle3;
        App.Cycle.Cycle2 += OnCycle2;
        App.Cycle.Cycle1 += OnCycle1;
    }

    private void OnCycle3()
    {
        if (isPowered)
        {
            // Debug.Log("cell lasers powering up");
        }
    }

    private void OnCycle2()
    {
        if (isPowered)
        {
            SetLasersActive(true);
        }
    }

    private void OnCycle1()
    {
        SetLasersActive(false);
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
        for(int i = 0; i < emitters.Length; ++i)
        {
            allLasers.Add(emitters[i]);
        }
    }
}
