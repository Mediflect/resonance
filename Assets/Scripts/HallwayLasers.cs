using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Medi;

public class HallwayLasers : MonoBehaviour
{
    public List<LaserEmitter> allLasers = new List<LaserEmitter>();

    private void Awake()
    {
        App.Request(OnAppExists);
        SetLasersActive(false);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycleStart;
    }

    private void OnCycleStart()
    {
        StartCoroutine(RunShootLasers());
    }

    private IEnumerator RunShootLasers()
    {
        SetLasersActive(true);
        yield return YieldInstructionCache.WaitForSeconds(3f);
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
        for (int i = 0; i < emitters.Length; ++i)
        {
            allLasers.Add(emitters[i]);
        }
    }
}
