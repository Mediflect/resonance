using System.Collections;
using UnityEngine;

public class TimestopPower : MonoBehaviour
{
    public TimestopFX effects;
    public bool isUnlocked = true;
    public bool isOnCooldown = false;
    public GameObject readyUi;

    private Coroutine currentCoroutine = null;

    private void Awake()
    {
        App.Request(OnAppExists);
        readyUi.SetActive(false);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += RefreshCooldown;
    }

    private void OnDestroy()
    {
        if (App.Exists)
        {
            App.Cycle.CycleStarted -= RefreshCooldown;
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
        readyUi.SetActive(true);
    }

    private void RefreshCooldown()
    {
        if (isUnlocked && isOnCooldown)
        {
            readyUi.SetActive(true);
            isOnCooldown = false;
        }
    }

    public void Activate()
    {
        if (isUnlocked && !isOnCooldown && currentCoroutine == null && !App.Cycle.IsStopped)
        {
            currentCoroutine = StartCoroutine(RunPower());
        }
    }

    private IEnumerator RunPower()
    {
        isOnCooldown = true;
        App.Cycle.Pause();
        yield return effects.PlayEffectsCoroutine();
        readyUi.SetActive(false);
        App.Cycle.Resume();
        currentCoroutine = null;
    }
}
