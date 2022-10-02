using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticktock : MonoBehaviour
{
    public AudioSource sound;

    private float timer = 0f;

    private void OnEnable()
    {
        timer = 1f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            timer -= 1f;
            sound.Play();
        }
    }
}
