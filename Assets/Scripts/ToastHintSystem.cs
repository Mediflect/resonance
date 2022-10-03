using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class ToastHintSystem : MonoBehaviour
{
    public bool IsPlayingHint { get; private set; } = false;

    public float fadeOutTime = 2.5f;
    public Typewriter typewriter;

    [SerializeField]
    private ToastHint testHint;

    private Queue<ToastHint> queuedHints = new Queue<ToastHint>(5);

    public void QueueHint(ToastHint hint)
    {
        queuedHints.Enqueue(hint);
        if (queuedHints.Count == 1)
        {
            IsPlayingHint = true;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(RunHintProcessing());
    }

    private IEnumerator RunHintProcessing()
    {
        while (true)
        {
            if (queuedHints.Count < 1)
            {
                yield return null;
                continue;
            }

            IsPlayingHint = true;
            ToastHint toastHint = queuedHints.Dequeue();
            foreach (string line in toastHint.lines)
            {
                typewriter.SetText(line);
                typewriter.Play();
                yield return typewriter.PlayCoroutine;
            }

            if (toastHint.endWithFade)
            {
                yield return Helpers.RunTextFade(typewriter.TextUi, fadeOutTime, fadeIn: false);
            }

            typewriter.Clear();
            Helpers.SetTextAlpha(typewriter.TextUi, 1f);
            IsPlayingHint = false;
        }
    }

    [ContextMenu("Test")]
    private void DebugTestHint()
    {
        QueueHint(testHint);
    }
}
