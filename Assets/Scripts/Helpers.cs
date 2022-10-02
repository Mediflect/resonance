using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;
using UnityEngine.Rendering;

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
}
