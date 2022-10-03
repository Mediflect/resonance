using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class ToastHintSystem : MonoBehaviour
{
    public bool IsPlayingHint { get; private set; } = false;

    public float fadeOutTime = 2.5f;
    public Typewriter typewriter;
    public AudioSource hintSound;

    [SerializeField]
    private ToastHint testHint;

    private Queue<ToastHint> queuedHints = new Queue<ToastHint>(5);
    private float hintBaseVolume;

    public void QueueHint(ToastHint hint)
    {
        queuedHints.Enqueue(hint);
        if (queuedHints.Count == 1)
        {
            IsPlayingHint = true;
        }
    }

    private void Awake()
    {
        hintBaseVolume = hintSound.volume;
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

            if (toastHint.playAudioOnStart)
            {
                StartCoroutine(Helpers.RunAudioFade(hintSound, fadeOutTime/2f, 0, hintBaseVolume));
                hintSound.Play();
            }

            foreach (string line in toastHint.lines)
            {
                typewriter.SetText(line);
                typewriter.Play();
                yield return typewriter.PlayCoroutine;
            }

            if (toastHint.endWithFade)
            {
                StartCoroutine(Helpers.RunAudioFade(hintSound, fadeOutTime, hintBaseVolume, 0f));
                yield return Helpers.RunTextFade(typewriter.TextUi, fadeOutTime, fadeIn: false);
                hintSound.Stop();
                hintSound.volume = hintBaseVolume;
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
