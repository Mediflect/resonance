using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public static class Helpers
{
    public static IEnumerator RunSmoothMoveTo(Transform obj, Transform start, Transform end, float transitionTime)
    {
        float timer = 0;
        while (timer < transitionTime)
        {
            float tValue = Mathf.InverseLerp(0, transitionTime, timer);
            float tValueSmoothed = Mathf.SmoothStep(0, 1, tValue);
            obj.position = Vector3.Lerp(start.position, end.position, tValueSmoothed);
            yield return null;
            timer += Time.deltaTime;
        }
    }

    public static IEnumerator RunVolumeWeightTransition(Volume volume, float duration, float startWeight, float endWeight, bool smooth = true)
    {
        float timer = 0;
        while (timer < duration)
        {
            float tValue = Mathf.InverseLerp(0, duration, timer);
            if (smooth)
            {
                Mathf.SmoothStep(startWeight, endWeight, tValue);
            }

            volume.weight = Mathf.Lerp(startWeight, endWeight, tValue);
            yield return null;
            timer += Time.deltaTime;
        }
    }

    public static IEnumerator RunTextFade(TextMeshProUGUI text, float duration, bool fadeIn, bool smooth = true)
    {
        float timer = 0;
        while (timer < duration)
        {
            float tValue = Mathf.InverseLerp(0, duration, timer);
            if (smooth)
            {
                tValue = Mathf.SmoothStep(0, 1, tValue);
            }

            SetTextAlpha(text, fadeIn ? tValue : 1 - tValue);
            yield return null;
            timer += Time.deltaTime;
        }
    }

    public static IEnumerator RunAudioFade(AudioSource source, float duration, float startVolume, float endVolume)
    {
        float timer = 0;
        while (timer < duration)
        {
            float tValue = Mathf.InverseLerp(0, duration, timer);
            source.volume = Mathf.Lerp(startVolume, endVolume, tValue);

            yield return null;
            timer += Time.deltaTime;
        }
    }

    public static void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    public static IEnumerator RunImageFade(RawImage image, float duration, float startAlpha, float endAlpha, bool smooth = true)
    {
        float timer = 0;
        while (timer < duration)
        {
            float tValue = Mathf.InverseLerp(0, duration, timer);
            if (smooth)
            {
                tValue = Mathf.SmoothStep(0, 1, tValue);
            }

            SetImageAlpha(image, Mathf.Lerp(startAlpha, endAlpha, tValue));
            yield return null;
            timer += Time.deltaTime;
        }
    }

    public static void SetImageAlpha(RawImage image, float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    public static IEnumerator RunDecayingPositionNoise(Transform transform, float duration, float noiseAmplitude, bool reverse = false)
    {
        Vector3 basePos = transform.localPosition;
        float timer = 0;
        while (timer < duration)
        {
            float tValue = Mathf.InverseLerp(0, duration, timer);
            float shakeAmplitude = Mathf.Lerp(noiseAmplitude, 0, tValue);
            if (reverse)
            {
                shakeAmplitude = Mathf.Lerp(0, noiseAmplitude, tValue);
            }
            transform.localPosition = new Vector3(Random.value * shakeAmplitude, Random.value * shakeAmplitude, Random.value * shakeAmplitude);
            yield return null;
            timer += Time.deltaTime;
        }
        transform.localPosition = basePos;
    }
}
