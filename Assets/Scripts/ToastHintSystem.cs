using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class ToastHintSystem : MonoBehaviour
{
    public float fadeOutTime = 2.5f;
    public Typewriter typewriter;

    [SerializeField]
    private ToastHint testHint;

    private Queue<ToastHint> queuedHints = new Queue<ToastHint>(5);

    public void QueueHint(ToastHint hint)
    {
        queuedHints.Enqueue(hint);
    }

    private void OnEnable()
    {
        StartCoroutine(RunHintProcessing());
    }

    private IEnumerator RunHintProcessing()
    {
        while(true)
        {
            if (queuedHints.Count < 1)
            {
                yield return null;
                continue;
            }

            ToastHint hint = queuedHints.Dequeue();
            typewriter.SetText(hint.hint);
            typewriter.Play();
            yield return typewriter.PlayCoroutine;
            yield return Helpers.RunTextFade(typewriter.TextUi, fadeOutTime, fadeIn: false);
        }
    }

    [ContextMenu("Test")]
    private void DebugTestHint()
    {
        QueueHint(testHint);
    }
}
