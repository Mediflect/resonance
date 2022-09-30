using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle : MonoBehaviour
{
    public event Action CycleStarted;

    public float duration = 10f;
    public bool startOnAwake = true;

    public float timer { get; private set; } = 0f;

    private void Awake()
    {
        if (startOnAwake)
        {
            timer = duration;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            timer -= duration;
            CycleStarted?.Invoke();
        }
    }
}
