using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticktock : MonoBehaviour
{
    public AudioSource sound;
    public bool isPaused = false;
    public float timer = 0f;

    private void OnEnable()
    {
        timer = 1f;
    }

    private void Update()
    {
        if (isPaused)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > 1f)
        {
            timer -= 1f;
            if (!App.Cycle.IsStopped)
            {
                sound.Play();
            }
        }
    }
}
