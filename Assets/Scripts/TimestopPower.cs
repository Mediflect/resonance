using System.Collections;
using UnityEngine;

public class TimestopPower : MonoBehaviour
{
    public TimestopFX effects;
    public bool isUnlocked = true;

    private Coroutine currentCoroutine = null;

    public void Activate()
    {
        if (isUnlocked && currentCoroutine == null && !App.Cycle.IsStopped)
        {
            StartCoroutine(RunPower());
        }
    }

    private IEnumerator RunPower()
    {
        App.Cycle.Pause();
        yield return effects.PlayEffectsCoroutine();
        App.Cycle.Resume();
        currentCoroutine = null;
    }
}
