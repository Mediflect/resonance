using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle : MonoBehaviour
{
    public event Action CycleStopped;
    public event Action CyclePaused;
    public event Action CycleResumed;
    public event Action CycleStarted;
    public event Action Cycle9;
    public event Action Cycle8;
    public event Action Cycle7;
    public event Action Cycle6;
    public event Action Cycle5;
    public event Action Cycle4;
    public event Action Cycle3;
    public event Action Cycle2;
    public event Action Cycle1;


    public bool IsPaused => !this.enabled;
    public bool IsStopped { get; private set; } = false;
    public float duration = 10f;
    public float startDelay = 2f;
    public float timer { get; private set; } = 0f;

    private bool isDelaying = false;

    [ContextMenu("Pause")]
    public void Pause()
    {
        if (IsStopped)
        {
            Debug.LogWarning("cycle is stopped, game is over");
            return;
        }
        enabled = false;
        CyclePaused?.Invoke();
    }

    [ContextMenu("Resume")]
    public void Resume()
    {
        if (IsStopped)
        {
            Debug.LogWarning("can't resume a stopped cycle, game is over");
            return;
        }
        enabled = true;
        CycleResumed?.Invoke();
    }

    public void Stop()
    {
        Debug.Log("cycle has stopped, game is over");
        IsStopped = true;
        enabled = false;
        CycleStopped?.Invoke();
    }

    private void Awake()
    {
        timer = startDelay;
        isDelaying = true;
    }

    private void Update()
    {
        float oldTimer = timer;
        timer -= Time.deltaTime;

        if (!isDelaying)
        {
            CheckSecondEvent(9f, Cycle9, timer, oldTimer);
            CheckSecondEvent(8f, Cycle8, timer, oldTimer);
            CheckSecondEvent(7f, Cycle7, timer, oldTimer);
            CheckSecondEvent(6f, Cycle6, timer, oldTimer);
            CheckSecondEvent(5f, Cycle5, timer, oldTimer);
            CheckSecondEvent(4f, Cycle4, timer, oldTimer);
            CheckSecondEvent(3f, Cycle3, timer, oldTimer);
            CheckSecondEvent(2f, Cycle2, timer, oldTimer);
            CheckSecondEvent(1f, Cycle1, timer, oldTimer);
        }

        if (timer <= 0)
        {
            isDelaying = false;
            timer += duration;
            CycleStarted?.Invoke();
        }
    }

    private void CheckSecondEvent(float second, Action callback, float timer, float oldTimer)
    {
        if (oldTimer > second && timer < second)
        {
            callback?.Invoke();
        }
    }
}
