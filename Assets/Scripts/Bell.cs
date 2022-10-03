using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class Bell : MonoBehaviour
{
    public AnimationCurve strikeCurve;
    public float strikeEffectDuration = 3f;
    public float strikeRotateDegrees = 45f;

    private Coroutine strikeCoroutine = null;

    public void StrikeFromPoint(Vector3 strikePos)
    {
        if (strikeCoroutine != null)
        {
            return;
        }
        StartCoroutine(RunStrike(strikePos));
    }

    private IEnumerator RunStrike(Vector3 strikePos)
    {
        Quaternion startRotation = transform.rotation;
        Vector3 strikeDir = strikePos.DirTo(transform.position).Flatten().normalized;
        Vector3 rotationAxis = Vector3.Cross(strikeDir, Vector3.up);  // damn i was almost right the first time
        Quaternion fullRotation = startRotation * Quaternion.AngleAxis(strikeRotateDegrees, rotationAxis);
        float timer = 0f;
        while (timer != strikeEffectDuration)
        {
            if (!enabled)
            {
                yield return null;
                continue;
            }
            float tValue = Mathf.InverseLerp(0, strikeEffectDuration, timer);
            tValue = strikeCurve.Evaluate(tValue);
            transform.rotation = Quaternion.SlerpUnclamped(startRotation, fullRotation, tValue);
            yield return null;
            timer += Time.deltaTime;
        }
        transform.rotation = startRotation;
        strikeCoroutine = null;
        yield break;
    }

    [ContextMenu("Test Strike")]
    private void DebugTestStrike()
    {
        StrikeFromPoint(transform.position + Vector3.forward);
    }
}
