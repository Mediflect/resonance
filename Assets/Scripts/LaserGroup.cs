using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Medi;

public class LaserGroup : MonoBehaviour
{
    public bool startActive = true;
    public List<LaserEmitter> allLasers = new List<LaserEmitter>();

    private void Awake()
    {
        SetLasersActive(startActive);
    }

    public void SetLasersActive(bool active)
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

    [ContextMenu("Turn On")]
    private void TurnOn() => SetLasersActive(true);

    [ContextMenu("Turn Off")]
    private void TurnOff() => SetLasersActive(false);
}
