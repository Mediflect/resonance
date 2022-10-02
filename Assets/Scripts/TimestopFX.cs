using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Medi;

public class TimestopFX : MonoBehaviour
{
    public float preEffectDuration = 1.2f;
    public float startTransitionDuration = 1f;
    public float holdDuration = 4f;
    public float endTransitionDuration = 1f;

    [Header("Sounds")]
    public AudioSource leadupSound;
    public float holdSoundDelay = 3.497f;
    public AudioSource holdSound;
    public Ticktock ticktock;

    private Coroutine currentCoroutine = null;

    public Coroutine PlayEffectsCoroutine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            leadupSound.Stop();
            holdSound.Stop();
            App.TimestopEffectVolume.weight = 0;
            App.PreTimestopEffectVolume.weight = 0;
        }
        return StartCoroutine(RunEffects());
    }

    private IEnumerator RunEffects()
    {
        StartCoroutine(RunStartSoundEffects());

        // In effect
        ticktock.isPaused = true;
        yield return RunVolumeWeightTransition(App.PreTimestopEffectVolume, preEffectDuration, 0, 1);
        App.PreTimestopEffectVolume.weight = 0;
        ticktock.timer = 0f; // skip the first one
        ticktock.isPaused = false;
        yield return RunVolumeWeightTransition(App.TimestopEffectVolume, startTransitionDuration, 0.01f, 1);

        // Hold
        yield return YieldInstructionCache.WaitForSeconds(holdDuration);

        // Out effect
        ticktock.isPaused = true;
        yield return RunVolumeWeightTransition(App.TimestopEffectVolume, endTransitionDuration / 2f, 1, 0.01f);
        App.TimestopEffectVolume.weight = 0f;
        yield return RunVolumeWeightTransition(App.PreTimestopEffectVolume, endTransitionDuration / 2f, 1, 0);
        holdSound.Stop();
        ticktock.isPaused = false;
        currentCoroutine = null;
    }

    private IEnumerator RunVolumeWeightTransition(Volume volume, float duration, float startWeight, float endWeight, bool smooth = true)
    {
        float timer = 0;
        while (timer < duration)
        {
            float tValue = Mathf.InverseLerp(0, duration, timer);
            if (smooth)
            {
                volume.weight = Mathf.SmoothStep(startWeight, endWeight, tValue);
            }
            else
            {
                volume.weight = tValue;
            }
            yield return null;
            timer += Time.deltaTime;
        }
    }

    private IEnumerator RunStartSoundEffects()
    {
        leadupSound.Play();
        yield return YieldInstructionCache.WaitForSeconds(holdSoundDelay);
        holdSound.Play();
    }
}
