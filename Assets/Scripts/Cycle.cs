using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle : MonoBehaviour
{
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


    public float duration = 10f;
    public bool startOnAwake = true;

    public float timer { get; private set; } = 0f;

    private void Awake()
    {
        if (startOnAwake)
        {
            timer = 0;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        float oldTimer = timer;
        timer -= Time.deltaTime;

        CheckSecondEvent(9f, Cycle9, timer, oldTimer);
        CheckSecondEvent(8f, Cycle8, timer, oldTimer);
        CheckSecondEvent(7f, Cycle7, timer, oldTimer);
        CheckSecondEvent(6f, Cycle6, timer, oldTimer);
        CheckSecondEvent(5f, Cycle5, timer, oldTimer);
        CheckSecondEvent(4f, Cycle4, timer, oldTimer);
        CheckSecondEvent(3f, Cycle3, timer, oldTimer);
        CheckSecondEvent(2f, Cycle2, timer, oldTimer);
        CheckSecondEvent(1f, Cycle1, timer, oldTimer);

        if (timer <= 0)
        {
            timer = duration;
            CycleStarted?.Invoke();
        }
    }

    private void CheckSecondEvent(float second, Action callback, float timer, float oldTimer)
    {
        if(oldTimer > second && timer < second)
        {
            callback?.Invoke();
        }
    }
}
