using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

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
}
